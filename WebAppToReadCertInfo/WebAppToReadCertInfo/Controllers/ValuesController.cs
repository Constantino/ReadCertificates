using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Web.Http;

namespace WebAppToReadCertInfo.Controllers
{
    public class ValuesController : ApiController
    {
        public X509Certificate2 GetCertificate(string thumbprint)
        {

            if (string.IsNullOrEmpty(thumbprint))
                throw new ArgumentNullException("thumbprint", "Argument 'thumbprint' cannot be 'null' or 'string.empty'");

            X509Certificate2 retVal = null;

            X509Store certStore = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            certStore.Open(OpenFlags.ReadOnly);

            X509Certificate2Collection certCollection = certStore.Certificates.Find(X509FindType.FindByThumbprint, thumbprint, false);

            if (certCollection.Count > 0)
            {
                retVal = certCollection[0];
            }

            certStore.Close();

            return retVal;
        }

        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get([FromUri]string certThumbPrint)
        {
            X509Certificate2 cert = GetCertificate(certThumbPrint);

            if (!Object.Equals(cert, null))
            {
                return cert.Issuer;
            }

            return "not found";

        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
