using API.Errors;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly StoreContext _db;

        public BuggyController(StoreContext db)
        {
            _db = db;
        }

        [HttpGet("notfound")]
        public ActionResult GetNotFoundRequest()
        {
            var thing = _db.Products.Find(42);
            if (thing is null) 
                return NotFound(new ApiErrorResponse(404));
            
            return Ok();
        }
        
        [HttpGet("servererror")]
        public ActionResult GetServerError()
        {
            var thing = _db.Products.Find(42);
            var accessNull = thing.ToString();
            
            return Ok();
        }

        [HttpGet("badrequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiErrorResponse(400));
        }

        [HttpGet("badrequest/{id}")]
        public ActionResult GetBadRequest(int id)
        {
            return Ok();
        }

        [HttpGet("maths")]
        public ActionResult<double> GetMathsError()
        {
            var ran = new Random();
            return 1 / Math.Floor(ran.NextDouble());
        }
    }
}