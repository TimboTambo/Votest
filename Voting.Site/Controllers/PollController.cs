using System.Web.Mvc;
using Voting.Services;
using System;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Voting.Hubs;
using Voting.Services.Models;

namespace Voting.Controllers
{
    public class PollController : Controller
    {
        private readonly IPollService _pollService;
        private readonly IPollHub _pollHub;

        public PollController(IPollService pollService, IPollHub pollHub)
        {
            _pollService = pollService;
            _pollHub = pollHub;
        }

        [HttpGet]
        public ActionResult Index(string errorMessage = null)
        {
            if (!string.IsNullOrWhiteSpace(errorMessage))
            {
                ViewBag.ErrorMessage = errorMessage;
            }
            return View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            var model = new CreatePollModel();
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(CreatePollModel model)
        {
            var userId = GetUserId();
            var pollId = _pollService.SavePoll(model, userId.Value);
            return RedirectToAction("ViewPoll", new {pollId = pollId});
        }

        private Guid? GetUserId()
        {
            var userId = User.Identity.GetUserId();
            if (userId.IsNullOrWhiteSpace())
            {
                return null;
            }
            return Guid.Parse(User.Identity.GetUserId());
        }

        [Authorize]
        [HttpGet]
        public ActionResult ViewAllPolls()
        {
            var polls = _pollService.GetAllUsersPolls(GetUserId().Value).ToList();
            return View("ViewAllPolls", polls);
        }

        [HttpGet]
        public ActionResult ViewPoll(int pollId)
        {
            var poll = _pollService.GetPoll(pollId);
            if (poll == null)
            {
                return new HttpNotFoundResult();
            }
            var userIsPollOwner = GetUserId() == poll.CreatedBy;
            if (poll.IsNotStarted)
            {
                return View("PollWaitingPage", poll.GetWaitingOnPollModel(userIsPollOwner));
            }
            if (userIsPollOwner || UserHasCompletedPoll(poll.PollId) || poll.IsExpired)
            {
                return View("PollResults", poll.GetPollResultsModel());
            }
            return View("CompletePoll", poll.GetCompletePollModel());
        }

        private bool UserHasCompletedPoll(int pollId)
        {
            if (ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("CompletedPolls"))
            {
                var pollsCookie = ControllerContext.HttpContext.Request.Cookies["CompletedPolls"];
                if (pollsCookie != null && pollsCookie.Value.Contains(pollId.ToString()))
                {
                    return true;
                }
            }
            return false;
        }

        private void UpdateCompletedPollsCookie(int pollId)
        {
            if (ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("CompletedPolls"))
            {
                var pollsCookie = ControllerContext.HttpContext.Request.Cookies["CompletedPolls"];
                pollsCookie.Value = pollsCookie.Value + "," + pollId;
                ControllerContext.HttpContext.Response.Cookies.Add(pollsCookie);
            }
            else
            {
                var cookie = new HttpCookie("CompletedPolls");
                cookie.Value = pollId.ToString();
                ControllerContext.HttpContext.Response.Cookies.Add(cookie);
            }
        }

        [HttpGet]
        public ActionResult FindPoll(string searchTerm)
        {
            var poll = _pollService.GetPoll(searchTerm);
            if (poll != null)
            {
                return RedirectToAction("ViewPoll", new { pollId = poll.PollId});
            }
            var errorMessage = string.Format("Poll could not be found with code: {0}", searchTerm);
            return RedirectToAction("Index", new { errorMessage});
        }

        [Authorize]
        [HttpPost]
        public ActionResult Start(WaitingOnPollModel model)
        {
            var poll = _pollService.StartPoll(model.PollId);
            _pollHub.StartPoll(poll.PollId);
            return RedirectToAction("ViewPoll", new { pollId = poll.PollId});
        }

        [HttpPost]
        public ActionResult CompletePoll(CompletePollModel response)
        {
            _pollService.SaveResponse(response);
            UpdateCompletedPollsCookie(response.PollId);
            return RedirectToAction("ViewPoll", new {pollId = response.PollId});
        }

        public ActionResult AddQuestion()
        {
            return PartialView("_NewQuestion", new NewQuestion());
        }

        public ActionResult AddOption(string containerPrefix)
        {
            ViewData["HtmlFieldPrefix"] = containerPrefix;
            return PartialView("_NewQuestionOption", new NewQuestionOption());
        }
    }
}