using CyberSource.Clients;
using CyberSource.Clients.NVPServiceReference;
using Newtonsoft.Json;
using Soriana.PPS.DecisionManager.Common.Constants;
using Soriana.PPS.DecisionManager.Common.Cybersource.Common;
using Soriana.PPS.DecisionManager.Common.DTO;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace Soriana.PPS.DecisionManager.Common.Helpers
{
    public static class NVPTransactionProcessorClientHelper
    {
        #region Constants
        public const string CYBS_SUBJECT_NAME = "CyberSource_SJC_US";
        public const string CYBERSOURCE_PUBLIC_KEY = "CyberSource_SJC_US";
        public const string CYBERSOURCE_P12_EXTENSION = ".p12";
        public const string CLIENT_LIBRARY_VERSION = "1.4.4";
        #endregion
        #region Private Fields
        private static string _ServerURL;
        private static bool _SendToProduction;
        private static bool _SendToAkamai;
        private static string _AkamaiProductionURL;
        private static string _ProductionURL;
        private static string _AkamaiTestURL;
        private static string _TestURL;
        private static string _MerchantID;
        private static string _Password;
        private static string _KeyFilename;
        private static ConcurrentDictionary<string, CertificateEntry> _MerchantIdentities = new ConcurrentDictionary<string, CertificateEntry>();
        private static string _MEnvironmentInfo = Environment.OSVersion.Platform + Environment.OSVersion.Version.ToString() + "-CLR" + Environment.Version.ToString();
        #endregion
        #region Private Properties
        private static string EffectiveServerURL
        {
            get
            {
                if (!string.IsNullOrEmpty(_ServerURL))
                    return _ServerURL;
                if (!_SendToProduction && !_SendToAkamai)
                    throw new ApplicationException(NVPTransactionProcessorClientConstants.CONFIGURATION_BUG_PRODUCTION_URL_OR_SERVER_URL);
                if (_SendToProduction)
                    return (_SendToAkamai ? _AkamaiProductionURL : _ProductionURL);
                return (_SendToAkamai ? _AkamaiTestURL : _TestURL);
            }
        }
        private static string EffectivePassword
        {
            get
            {
                return (!string.IsNullOrEmpty(_Password) ? _Password : _MerchantID);
            }
        }
        private static string EffectiveKeyFilename
        {
            get
            {
                return !string.IsNullOrEmpty(_KeyFilename) ? _KeyFilename : _MerchantID + CYBERSOURCE_P12_EXTENSION;
            }
        }
        #endregion
        #region Private Methods
        private static void SetConnectionLimit(dynamic cybersourceOptions)
        {
            if (cybersourceOptions.ConnectionLimit != -1)
            {
                Uri uri = new Uri(EffectiveServerURL);
                ServicePoint servicePoint = ServicePointManager.FindServicePoint(uri);
                servicePoint.ConnectionLimit = cybersourceOptions.ConnectionLimit;
            }
        }
        private static bool IsExpiredMerchantCertificate(string merchantId, DateTime modifiedTime, ConcurrentDictionary<string, CertificateEntry> merchantIdentities)
        {
            if (merchantIdentities[merchantId] != null)
            {
                if (merchantIdentities[merchantId].ModifiedTime != modifiedTime)
                    throw new Exception(NVPTransactionProcessorClientConstants.CONFIGURATION_BUG_CERTIFICATE_IS_EXPIRED);
            }
            return false;
        }
        private static X509Certificate2 GetOrFindValidMerchantCertFromStore(string merchantId, ConcurrentDictionary<string, CertificateEntry> merchantIdentities)
        {
            return merchantIdentities[merchantId] != null ? merchantIdentities[merchantId].MerchantCert : null;
        }
        private static X509Certificate2 GetOrFindValidCybsCertFromStore(string merchantId, ConcurrentDictionary<string, CertificateEntry> merchantIdentities)
        {
            return merchantIdentities[merchantId] != null ? merchantIdentities[merchantId].CybsCert : null;
        }
        // will return an empty string if the hashtable is empty.
        private static string Hash2String(Hashtable src)
        {
            StringBuilder dest = new StringBuilder();
            foreach (string key in src.Keys)
                dest.AppendFormat("{0}={1}\n", key, src[key]);
            return (dest.ToString());
        }
        // will return an empty hashtable if the string is empty.
        private static Hashtable String2Hash(string src)
        {
            char[] EQUAL_SIGN = { '=' };
            Hashtable dest = new Hashtable();
            StringReader reader = new StringReader(src);
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] parts = line.Split(EQUAL_SIGN, 2);
                if (parts.Length > 0)
                    dest.Add(parts[0], parts.Length == 2 ? parts[1] : null);
            }
            return (dest);
        }
        private static void SetVersionInformation(Hashtable request)
        {
            request["clientLibrary"] = ".NET NVP";
            request["clientLibraryVersion"] = CLIENT_LIBRARY_VERSION;
            request["clientEnvironment"] = _MEnvironmentInfo;
            request["clientSecurityLibraryVersion"] = ".Net 1.4.4";
        }
        private static void ClearVariables()
        {
            _ServerURL = string.Empty;
            _SendToProduction = false;
            _SendToAkamai = false;
            _AkamaiProductionURL = string.Empty;
            _ProductionURL = string.Empty;
            _AkamaiTestURL = string.Empty;
            _TestURL = string.Empty;
            _MerchantID = string.Empty;
            _Password = string.Empty;
            _KeyFilename = string.Empty;
            _MerchantIdentities = new ConcurrentDictionary<string, CertificateEntry>();
        }
        private static void SetEffectiveServerURL(dynamic cybersourceOptions)
        {
            _ServerURL = cybersourceOptions.ServerURL;
            _SendToProduction = cybersourceOptions.SendToProduction;
            _SendToAkamai = cybersourceOptions.SendToAkamai;
            _AkamaiProductionURL = cybersourceOptions.AkamaiProductionURL;
            _ProductionURL = cybersourceOptions.ProductionURL;
            _AkamaiTestURL = cybersourceOptions.AkamaiTestURL;
            _TestURL = cybersourceOptions.TestURL;
        }
        private static void SetEffectivePassword(dynamic cybersourceOptions)
        {
            _Password = cybersourceOptions.Password;
            _MerchantID = cybersourceOptions.MerchantID;
        }
        private static void SetEffectiveKeyFilename(dynamic cybersourceOptions)
        {
            _KeyFilename = cybersourceOptions.KeyFilename;
        }
        private static Hashtable GetInitializeRequest(dynamic cybersourceOptions, dynamic notifyAuthenticationRequest)
        {
            Hashtable request = new Hashtable();
            request.Add(NVPTransactionProcessorClientConstants.NOTIFY_VALIDATION_REQUEST_ACTION_SERVICE_RUN, "true");
            request.Add(NVPTransactionProcessorClientConstants.NOTIFY_VALIDATION_REQUEST_MERCHANT_REFERENCE_CODE, notifyAuthenticationRequest.MerchantReferenceCode);
            request.Add(NVPTransactionProcessorClientConstants.NOTIFY_VALIDATION_REQUEST_ACTION_SERVICE_ACTION_CODE, notifyAuthenticationRequest.ActionCode);
            request.Add(NVPTransactionProcessorClientConstants.NOTIFY_VALIDATION_REQUEST_ACTION_SERVICE_REQUEST_ID, notifyAuthenticationRequest.RequestID);
            request.Add(NVPTransactionProcessorClientConstants.NOTIFY_VALIDATION_REQUEST_ACTION_SERVICE_COMMENTS, notifyAuthenticationRequest.Comments);
            return request;
        }
        private static string EnumerateValues(Hashtable reply, string fieldName)
        {
            StringBuilder sb = new StringBuilder();
            string val = "";
            for (int i = 0; val != null; ++i)
            {
                val = (string)reply[fieldName + "_" + i];
                if (val != null)
                    sb.Append(val + "\n");
            }
            return (sb.ToString());
        }
        private static string GetContent(Hashtable reply)
        {
            /*
			 * This is where you retrieve the content that will be plugged
			 * into the template.
			 * 
			 * The strings returned in this sample are mostly to demonstrate
			 * how to retrieve the reply fields.  Your application should
			 * display user-friendly messages.
			 */
            int reasonCode = int.Parse((string)reply["reasonCode"]);
            switch (reasonCode)
            {
                // Success
                case 100:
                    return JsonConvert.SerializeObject(new NotifyAuthenticationResponse()
                    {
                        ReasonCode = reasonCode,
                        RequestID = (string)reply["requestID"],
                        AuthorizationCode = (string)reply["ccAuthReply_authorizationCode"],
                        CaptureRequestTime = (string)reply["ccCaptureReply_requestDateTime"],
                        CapturedAmount = (string)reply["ccCaptureReply_amount"],
                        HasError = false
                    });
                // Missing field(s)
                case 101:
                    return JsonConvert.SerializeObject(new NotifyAuthenticationResponse()
                    {
                        ReasonCode = reasonCode,
                        HasError = true,
                        ErrorDetails = string.Format("The following required field(s) are missing: {0}", EnumerateValues(reply, "missingField"))
                    });
                // Invalid field(s)
                case 102:
                    return JsonConvert.SerializeObject(new NotifyAuthenticationResponse()
                    {
                        ReasonCode = reasonCode,
                        HasError = true,
                        ErrorDetails = string.Format("The following field(s) are invalid: {0}", EnumerateValues(reply, "invalidField"))
                    });
                // Insufficient funds
                case 204:
                    return JsonConvert.SerializeObject(new NotifyAuthenticationResponse()
                    {
                        ReasonCode = reasonCode,
                        HasError = true,
                        ErrorDetails = "Insufficient funds in the account. Please use a different card or select another form of payment."
                    });
                // add additional reason codes here that you need to handle
                // specifically.
                default:
                    // For all other reason codes, return an empty string,
                    // in which case, the template will be displayed with no
                    // specific content.
                    return JsonConvert.SerializeObject(new NotifyAuthenticationResponse()
                    {
                        ReasonCode = reasonCode
                    });
            }
        }
        #endregion
        #region Public Methods
        public static void SetEffectiveVariables(dynamic cybersourceOptions)
        {
            SetEffectiveServerURL(cybersourceOptions);
            SetEffectivePassword(cybersourceOptions);
            SetEffectiveKeyFilename(cybersourceOptions);
        }
        public static async Task<string> RunTransaction(dynamic cybersourceOptions, dynamic notifyAuthenticationRequest)
        {
            Hashtable request = GetInitializeRequest(cybersourceOptions, notifyAuthenticationRequest);
            SetVersionInformation(request);
            SetConnectionLimit(cybersourceOptions);
            //Setup custom binding with HTTPS + Body Signing 
            CustomBinding currentBinding = CustomBindingHelper.GetWCFCustomBinding(cybersourceOptions);
            //Setup endpoint Address with dns identity
            AddressHeaderCollection headers = new AddressHeaderCollection();
            EndpointAddress endpointAddress = new EndpointAddress(new Uri(EffectiveServerURL), EndpointIdentity.CreateDnsIdentity(EffectivePassword), headers);
            //Get instance of service
            using (NVPTransactionProcessorClient nVPTransactionProcessorClient = new NVPTransactionProcessorClient(currentBinding, endpointAddress))
            {
                try
                {
                    // set the timeout
                    TimeSpan timeOut = new TimeSpan(0, 0, 0, cybersourceOptions.Timeout, 0);
                    currentBinding.SendTimeout = timeOut;
                    string keyFilePath = Path.Combine(cybersourceOptions.KeysDirectory, EffectiveKeyFilename);
                    X509Certificate2 merchantCert = null, cybsCert = null;
                    DateTime dateFile = File.GetLastWriteTime(keyFilePath);
                    X509Certificate2Collection collection = new X509Certificate2Collection();
                    if (cybersourceOptions.CertificateCacheEnabled)
                    {
                        if (!_MerchantIdentities.ContainsKey(cybersourceOptions.KeyAlias) || IsExpiredMerchantCertificate(cybersourceOptions.KeyAlias, dateFile, _MerchantIdentities))
                        {
                            collection.Import(keyFilePath, EffectivePassword, X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.Exportable | X509KeyStorageFlags.PersistKeySet);
                            X509Certificate2 newMerchantCert = null, newCybsCert = null;
                            foreach (X509Certificate2 cert1 in collection)
                            {
                                if (cert1.Subject.Contains(cybersourceOptions.KeyAlias))
                                    newMerchantCert = cert1;
                                if (cert1.Subject.Contains(CYBS_SUBJECT_NAME))
                                    newCybsCert = cert1;
                            }
                            CertificateEntry newCert = new CertificateEntry { ModifiedTime = dateFile, CybsCert = newCybsCert, MerchantCert = newMerchantCert };
                            _MerchantIdentities.AddOrUpdate(cybersourceOptions.KeyAlias as string, newCert, (x, y) => newCert);
                        }
                        merchantCert = GetOrFindValidMerchantCertFromStore(cybersourceOptions.KeyAlias, _MerchantIdentities);
                        if (cybersourceOptions.UseSignedAndEncrypted)
                        {
                            cybsCert = GetOrFindValidCybsCertFromStore(cybersourceOptions.KeyAlias, _MerchantIdentities);
                        }
                    }
                    else
                    {
                        // Changes for SHA2 certificates support
                        collection.Import(keyFilePath, EffectivePassword, X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.Exportable | X509KeyStorageFlags.PersistKeySet);
                        foreach (X509Certificate2 cert1 in collection)
                        {
                            if (cert1.Subject.Contains(cybersourceOptions.KeyAlias))
                            {
                                merchantCert = cert1;
                                break;
                            }
                        }
                        if (cybersourceOptions.UseSignedAndEncrypted)
                        {
                            foreach (X509Certificate2 cert2 in collection)
                            {
                                if (cert2.Subject.Contains(CYBERSOURCE_PUBLIC_KEY))
                                {
                                    cybsCert = cert2;
                                    break;
                                }
                            }
                        }
                    }
                    if (merchantCert == null)
                        throw new ApplicationException(NVPTransactionProcessorClientConstants.CONFIGURATION_OR_CODE_BUG_MERCHANT_CERTIFICATE_IS_MISSING);
                    //Set protection level to sign
                    nVPTransactionProcessorClient.Endpoint.Contract.ProtectionLevel = System.Net.Security.ProtectionLevel.Sign;
                    nVPTransactionProcessorClient.ClientCredentials.ServiceCertificate.Authentication.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.None;
                    nVPTransactionProcessorClient.ClientCredentials.ClientCertificate.Certificate = merchantCert;
                    nVPTransactionProcessorClient.ClientCredentials.ServiceCertificate.DefaultCertificate = merchantCert;
                    if (cybersourceOptions.UseSignedAndEncrypted)
                    {
                        if (cybsCert == null)
                            throw new ApplicationException(NVPTransactionProcessorClientConstants.CONFIGURATION_OR_CODE_BUG_CYBS_CERTIFICATE_IS_MISSING);
                        //Set protection level to sign & encrypt only
                        nVPTransactionProcessorClient.Endpoint.Contract.ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign;
                        nVPTransactionProcessorClient.ClientCredentials.ServiceCertificate.DefaultCertificate = cybsCert;
                    }
                    // send request now, converting the hashtable request into
                    // a string, and the string reply back into a hashtable.
                    string response = await Task.Run(() => nVPTransactionProcessorClient.runTransaction(Hash2String(request)));
                    Hashtable reply = String2Hash(response);
                    return GetContent(reply);
                }
                catch (CybersourceFaultException ex)
                {
                    nVPTransactionProcessorClient.Abort();
                    throw ex;
                }
                catch (Exception ex)
                {
                    nVPTransactionProcessorClient.Abort();
                    throw ex;
                }
                finally
                {
                    ClearVariables();
                    nVPTransactionProcessorClient.Close();
                }
            }
        }
        #endregion
    }
}
