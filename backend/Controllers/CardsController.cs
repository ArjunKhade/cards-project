using Cards.API.data;
using Cards.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cards.API.Controllers
{
    [ApiController]
    [Route("/api/cards")]
    public class CardsController : Controller
    {
        private readonly CardsDbContext cardsDbContext;
        public CardsController(CardsDbContext cardsDbContext)
        {
            this.cardsDbContext= cardsDbContext;
        }


        //get all cards
        [HttpGet]
        public async Task<IActionResult> getAllCards()
        {
            var cards = await cardsDbContext.Cards.ToListAsync();
            return Ok(cards);
        }

        //get  card
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("getCard")]
        public async Task<IActionResult> getCard([FromRoute] Guid id)
        {
            var card = await cardsDbContext.Cards.FirstOrDefaultAsync(x => x.Id == id);
            if (card != null)
            {
                return Ok(card);
            }
            return NotFound("Card Not found!");
            
        }

        //add  card
        [HttpPost]

        public async Task<IActionResult> addCard ([FromBody] Card card)
        {
            card.Id= Guid.NewGuid();
            await cardsDbContext.Cards.AddAsync(card);
            await cardsDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(getCard),new { id = card.Id }, card);

        }


        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> updateCard ([FromBody] Card card , [FromRoute] Guid id)
        {
            var existing_card = await cardsDbContext.Cards.FirstOrDefaultAsync(x => x.Id == id);

            if(existing_card != null)
            {
                existing_card.CardHolderName = card.CardHolderName;
                existing_card.CardNumber = card.CardNumber;
                existing_card.ExpiryMonth = card.ExpiryMonth;
                existing_card.ExpiryYear = card.ExpiryYear;
                existing_card.ExpiryMonth= card.ExpiryMonth;

                await cardsDbContext.SaveChangesAsync();

                return Ok(existing_card);
            }

            return NotFound("Card Not Found");

        }


        //delete  card
        [HttpDelete]
        [Route("{id:guid}")]
   
        public async Task<IActionResult> deleteCard([FromRoute] Guid id)
        {
            var card_exist = await cardsDbContext.Cards.FirstOrDefaultAsync(x => x.Id == id);
            if (card_exist != null)
            {
                cardsDbContext.Remove(card_exist);
                await cardsDbContext.SaveChangesAsync();
                return Ok(card_exist);
            }
            return NotFound("Card Not Exist!");

        }


    }
}
