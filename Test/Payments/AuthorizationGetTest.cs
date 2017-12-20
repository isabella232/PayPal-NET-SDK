using System;
using System.IO;
using System.Text;
using System.Net.Http;
using System.Collections.Generic;
using BraintreeHttp;
using Xunit;
using PayPal.Test;
using static PayPal.Test.TestHarness;
using static PayPal.Payments.Test.PaymentCreateTest;

namespace PayPal.Payments.Test
{
    [Collection("Payments")]
    public class AuthorizationGetTest
    {

        [Fact]
        public async void TestAuthorizationGetRequest()
        {
            var client = TestHarness.client();

            var paymentCreateReponse = await PaymentCreateTest.CreatePayment("authorize");
            var authId = paymentCreateReponse.Result<Payment>()
                .Transactions[0]
                .RelatedResources[0]
                .Authorization.Id;
            AuthorizationGetRequest request = new AuthorizationGetRequest(authId);

            HttpResponse response = await client.Execute(request);
            Assert.Equal((int) response.StatusCode, 200);
            Assert.NotNull(response.Result<Authorization>());
        }
    }
}
