using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookService.Models;
using Microsoft.EntityFrameworkCore;

// used tutorial found here to build this Api: https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api

namespace BookService.Controllers
{
    [Route("[controller]")]
    public class BooksController : Controller
    {
        AngularWorkshopContext _context;

        public BooksController(AngularWorkshopContext context)
        {
            _context = context;
        }


        // GET books/getbooks
        [HttpGet]
        [Route("getbooks")]
        public async Task<IEnumerable<Book>> Get()
        {
            return await _context.Book.ToListAsync();
        }

        // GET books/getbooks/5
        //[HttpGet]
        [Route("getbooks/{id}")]
        [HttpGet]
        public IActionResult Get(int id)
        {
            var book = _context.Book.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            return new ObjectResult(book);
        }

        // POST books/addbook
        [HttpPost]
        [Route("addbook")]
        public IActionResult Post([FromBody]Book book)
        {
            if (book == null)
            {
                return BadRequest();
            }
            
            _context.Add(book);
            _context.SaveChanges();

            return CreatedAtAction("Get",new { id = book.Id }, book);
        }

        // Put books/modifyook
        [HttpPut]
        [Route("modifybook")]
        public IActionResult Put([FromBody]Book bookToLookup)
        {
            if (bookToLookup == null)
            {
                return BadRequest();
            }

            var book = _context.Book.FirstOrDefault(b => b.Id == bookToLookup.Id);
            if (book == null)
            {
                return NotFound();
            }

            book.Id = bookToLookup.Id;
            book.Title= bookToLookup.Title;
            book.Author = bookToLookup.Author;
            book.IsCheckedOut = bookToLookup.IsCheckedOut;
            book.Rating= bookToLookup.Rating;

            _context.Book.Update(book);
            _context.SaveChanges();
            return new NoContentResult();
        }

        // DELETE books/deletebook/1
        [HttpDelete]
        [Route("deletebook/{id}")]
        public IActionResult Delete(int id)
        {
            var book = _context.Book.FirstOrDefault(b => b.Id == id);
            if (book== null)
            {
                return NotFound();
            }

            _context.Book.Remove(book);
            _context.SaveChanges();
            return new NoContentResult();
        }


        // GET books/GetNextId
        [HttpGet]
        [Route("GetNextId")]
        public int GetNextId()
        {
            return _context.Book.Count() == 0 ? 1 : _context.Book.OrderByDescending(b => b.Id).FirstOrDefault().Id + 1;
        }

        // GET books/canActivate
        [HttpGet]
        [Route("canactivate/{id}")]
        public bool CanActivate(int id)
        {
            return _context.Book.FirstOrDefault(b => b.Id == id) != null ? true : false;
        }

    }
}
