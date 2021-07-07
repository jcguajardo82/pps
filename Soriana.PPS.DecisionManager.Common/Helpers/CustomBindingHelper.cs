using CyberSource.Clients;
using Soriana.PPS.DecisionManager.Common.Cybersource;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Security.Tokens;

namespace Soriana.PPS.DecisionManager.Common.Helpers
{
    public class CustomBindingHelper
    {
        #region Public Methods
        public static CustomBinding GetWCFCustomBinding(dynamic cybersourceOptions, WebProxy webProxy = null)
        {
            //Setup custom binding with HTTPS + Body Signing 
            CustomBinding currentBinding = new CustomBinding();
            //Sign the body
            AsymmetricSecurityBindingElement asec = (AsymmetricSecurityBindingElement)SecurityBindingElement.CreateMutualCertificateDuplexBindingElement(MessageSecurityVersion.WSSecurity10WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11BasicSecurityProfile10);
            asec.SetKeyDerivation(false);
            asec.IncludeTimestamp = false;
            asec.EnableUnsecuredResponse = true;
            asec.SecurityHeaderLayout = SecurityHeaderLayout.Lax;
            if (cybersourceOptions.UseSignedAndEncrypted)
            {
                asec.LocalClientSettings.IdentityVerifier = new CustomIdentityVerifier();
                asec.RecipientTokenParameters = new X509SecurityTokenParameters { InclusionMode = SecurityTokenInclusionMode.Once };
                asec.MessageProtectionOrder = System.ServiceModel.Security.MessageProtectionOrder.SignBeforeEncrypt;
                asec.EndpointSupportingTokenParameters.SignedEncrypted.Add(new System.ServiceModel.Security.Tokens.X509SecurityTokenParameters());
                asec.SetKeyDerivation(false);
            }
            //Use custom encoder to strip unsigned timestamp in response
            CustomTextMessageBindingElement textBindingElement = new CustomTextMessageBindingElement();
            //Setup https transport 
            HttpsTransportBindingElement httpsTransport = new HttpsTransportBindingElement();
            httpsTransport.RequireClientCertificate = true;
            httpsTransport.AuthenticationScheme = AuthenticationSchemes.Anonymous;
            httpsTransport.MaxReceivedMessageSize = 2147483647;
            httpsTransport.UseDefaultWebProxy = false;
            //Setup Proxy if needed
            if (webProxy != null)
            {
                WebRequest.DefaultWebProxy = webProxy;
                httpsTransport.UseDefaultWebProxy = true;
            }
            //Bind in order (Security layer, message layer, transport layer)
            currentBinding.Elements.Add(asec);
            currentBinding.Elements.Add(textBindingElement);
            currentBinding.Elements.Add(httpsTransport);
            return currentBinding;
        }
        #endregion
    }
}
