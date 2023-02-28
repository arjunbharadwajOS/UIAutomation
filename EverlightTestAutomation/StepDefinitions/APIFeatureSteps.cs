namespace EverlightTestAutomation.StepDefinitions
{
    [Binding]
    public class APIFeatureSteps
    {
        APIMethods method = new APIMethods();
        RestResponse? response = null;
        Random rnd1 = new Random(3);
        private int rnd1Id = 0;
        static List<string> idList = new List<string>();

        [Given(@"I access the endpoint using URL ""([^""]*)""")]
        [When(@"I access the endpoint using URL ""([^""]*)""")]
        [Then(@"I access the endpoint using URL ""([^""]*)""")]
        public void IAccessTheEndpointUsingURL(string URL)
        {
            response = method.GetMethod(URL);

            Assert.IsTrue(response.IsSuccessful, response.IsSuccessStatusCode.ToString());
            Assert.IsTrue(response.ContentLength > 0, "response content has value");
        }

        [Given(@"verify the schema of the api response with input file ""([^""]*)""")]
        [When(@"verify the schema of the api response with input file ""([^""]*)""")]
        [Then(@"verify the schema of the api response with input file ""([^""]*)""")]
        public async Task GivenVerifyTheSchemaOfTheApiResponseWithInputFileAsync(string jsonFile)
        {
            string workingDirectory = Environment.CurrentDirectory;
            string schemaContent = File.ReadAllText(workingDirectory + @"\Schema\" + jsonFile);

            var schemaObj = await JsonSchema.FromJsonAsync(schemaContent);

            var jObjectResponse = response.Content.ToString();

            var errorsForValidJson = schemaObj.Validate(jObjectResponse);

            Assert.IsTrue(errorsForValidJson.Count == 0, "Json Schema is valid");
        }

        [Given(@"I access the create order using URL ""([^""]*)""")]
        public void GivenIAccessTheCreateOrderUsingURL(string URL)
        {
            rnd1Id = rnd1.Next(100, 1000);

            string body = "{\"patientMrn\":\"P" + rnd1Id.ToString() + "\",\"patientFirstName\":\"firstname\",\"patientLastName\":\"lastname\",\"accessionNumber\":\"00" + rnd1Id.ToString() + "\",\"orgCode\":\"LUM\",\"siteId\":\"101\",\"modality\":\"MR\",\"studyDateTime\":\"" + DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "\"}";
            response = method.PostMethod(URL, body);

            Assert.IsTrue(response.IsSuccessful, response.IsSuccessStatusCode.ToString());
         
        }

        [When(@"I access the view order using URL ""([^""]*)""")]
        public void WhenIAccessTheViewOrderUsingURL(string URL)
        {
            response = method.GetMethod(URL);

            Assert.IsTrue(response.IsSuccessful, response.IsSuccessStatusCode.ToString());
            Assert.IsTrue(response.ContentLength > 0, "response content has value");
        }

        [Then(@"I delete order using URL ""([^""]*)""")]
        public void ThenIDeleteOrderUsingURL(string URL)
        {
            JArray jArray = JArray.Parse(json: response.Content.ToString());

            foreach (JObject item in jArray)
            {
                idList.Add(item.GetValue("id").ToString());
            }

            response = method.DeleteMethod(URL + idList[idList.Count - 1]);
            Assert.IsTrue(response.IsSuccessful, response.StatusCode.ToString());

        }

        [Given(@"I access create order all field validations using URL ""([^""]*)""")]
        public void GivenIAccessCreateOrderAllFieldValidationsUsingURL(string URL)
        {
            string body = @"{" + "\n" +@"  ""patientMrn"": """"," + "\n" +@"  ""patientFirstName"": """"," + "\n" +@"  ""patientLastName"": """"," + "\n" +@"  ""accessionNumber"": """"," + "\n" +@"  ""orgCode"": """"," + "\n" +@"  ""siteId"": """"," + "\n" +@"  ""modality"": """"," + "\n" +@"  ""studyDateTime"": """"" + "\n" +@"}";
            response = method.PostMethod(URL, body);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [When(@"I access create order with existing ""([^""]*)"" validation using URL ""([^""]*)""")]
        public void WhenIAccessCreateOrderWithExistingValidationUsingURL(string accessionNumber, string URL)
        {
            rnd1Id = rnd1.Next(100, 1000);

            string body = "{\"patientMrn\":\"P" + rnd1Id.ToString() + "\",\"patientFirstName\":\"firstname\",\"patientLastName\":\"lastname\",\"accessionNumber\":\"" + accessionNumber  +  "\",\"orgCode\":\"LUM\",\"siteId\":\"101\",\"modality\":\"MR\",\"studyDateTime\":\"" + DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "\"}";
            response = method.PostMethod(URL, body);

            Assert.AreEqual(HttpStatusCode.Conflict, response.StatusCode);
        }


        [Then(@"I delete order using URL ""([^""]*)"" with invalid id ""([^""]*)""")]
        public void ThenIDeleteOrderUsingURLWithInvalidId(string URL, string invalidId)
        {
            response = method.DeleteMethod(URL + invalidId);
            Assert.IsFalse(true, HttpStatusCode.NoContent.ToString());
        }



    }
}
