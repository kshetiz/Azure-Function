

using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using System.Security.Claims;

namespace Test
{
    public class MockHttpRequestData : HttpRequestData 
    {
        private readonly MemoryStream _bodyStream;

        public MockHttpRequestData(byte[] body, FunctionContext context) : base(context) 
     
        {
            _bodyStream = new MemoryStream(body);
        }

        public override Stream Body => _bodyStream;

        public override HttpHeadersCollection Headers => throw new NotImplementedException();

        public override IReadOnlyCollection<IHttpCookie> Cookies => throw new NotImplementedException();

        public override Uri Url => throw new NotImplementedException();

        public override IEnumerable<ClaimsIdentity> Identities => throw new NotImplementedException();

        public override string Method => throw new NotImplementedException();

        public override HttpResponseData CreateResponse()
        {
            throw new NotImplementedException();
        }
    }
}
