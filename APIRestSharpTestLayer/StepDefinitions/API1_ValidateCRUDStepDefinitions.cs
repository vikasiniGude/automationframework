using System;
using TechTalk.SpecFlow;
using APIRestSharpBusinessLayer.Utils;
using APIRestSharpBusinessLayer.Requests;
using NUnit.Framework;
using APIRestSharpCoreLayer.Utils;
using System.Net;
namespace APIRestSharpTestLayer.StepDefinitions
{
    [Binding]
    public class API1_ValidateCRUDStepDefinitions
    {
        public readonly ScenarioContext ScenarioContext;
        public TypiCode typicode;
        public API1_ValidateCRUDStepDefinitions(ScenarioContext scenarioContext)
        {
            ScenarioContext = scenarioContext;
            typicode = new TypiCode();

        }

        [Given(@"I create a new post uisng user id (\d+) id (\d+) title (.*) and body (.*)")]
        public void GivenICreateANewPostUisngUserIdIdTitleAndBody(int userId, int id, string title, string body)
        {
            try
            {
                RequestData output = TypiCode.CreateUserData(userId, id, title, body);
                Assert.That(output.userId, Is.EqualTo(userId));
                Assert.That(output.id, Is.EqualTo(id));
            }
            catch (Exception ex)
            {
                Log4NetLogger.Error($"Failed: at GivenICreateANewPostUisngUserIdIdTitleAndBody- {ex.Message}");
                Assert.Fail($"Failed: at GivenICreateANewPostUisngUserIdIdTitleAndBody- {ex.Message}");
            }
        }
        [When(@"I update a created post with user id '([^']*)' title '([^']*)'")]
        public void WhenIUpdateACreatedPostWithUserIdTitle(int userId, string title)
        {
            try
            {
                string titleResponse = TypiCode.UpdateUserDataForUserId(userId, title);
                Assert.That(titleResponse, Is.EqualTo(title));
            }
            catch (Exception ex)
            {
                Log4NetLogger.Error($"Failed: at WhenIUpdateACreatedPostWithUserIdTitle- {ex.Message}");
                Assert.Fail($"Failed: at WhenIUpdateACreatedPostWithUserIdTitle- {ex.Message}");
            }
        }

        [Then(@"I delete the created post with id '([^']*)'")]
        public void ThenIDeleteTheCreatedPostWithId(string userId)
        {
            try
            {
                bool isDeleted = TypiCode.DeletePost(userId, HttpStatusCode.OK);
                Assert.True(isDeleted, $"User Id {userId} is not deleted");
            }
            catch (Exception ex)
            {
                Log4NetLogger.Error($"Failed: at ThenIDeleteTheCreatedPostWithId- {ex.Message}");
                Assert.Fail($"Failed: at ThenIDeleteTheCreatedPostWithId- {ex.Message}");
            }
        }

        [When(@"I get the requested post with user id '([^']*)'")]
        public void WhenIGetTheRequestedPostWithUserId(int userId)
        {
            try
            {
                RequestData data = TypiCode.GetUserDataForUserId<RequestData>(userId);
                Assert.That(data, Is.Null.Or.Empty, $"User Id {userId} is not available");
            }
            catch (Exception ex)
            {
                Log4NetLogger.Error($"Failed: at WhenIGetTheRequestedPostWithUserId- {ex.Message}");
                Assert.Fail($"Failed: at WhenIGetTheRequestedPostWithUserId- {ex.Message}");
            }
        }

    }
}
