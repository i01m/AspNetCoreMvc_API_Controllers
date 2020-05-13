using Microsoft.AspNetCore.Mvc;
using ApiControllers.Models;


namespace ApiControllers.Controllers
{
    [Route("api/[controller]")]
    public class ContentController : Controller
    {
        [HttpGet("string")]
        public string GetString() => "This is string response";

        [HttpGet("object/{format?}")]
        [FormatFilter]//overrides any formats specified by the Accept header in a request (gives developer more control)
        //[Produces("application/json", "application/xml")] //specify format in response - overrides content negotiation process
        public Reservation GetObject() => new Reservation
        {
            ReservationId = 100,
            ClientName = "Jimmy",
            Location = "Board room"
        };

        [HttpPost]
        [Consumes("application/json")]
        public Reservation ReceiveJson([FromBody] Reservation reservation)
        {
            reservation.ClientName = "Json";
            return reservation;
        }

        [HttpPost]
        [Consumes("application/xml")]
        public Reservation ReceiveXml([FromBody] Reservation reservation)
        {
            reservation.ClientName = "Xml";
            return reservation;
        }
    }
}
