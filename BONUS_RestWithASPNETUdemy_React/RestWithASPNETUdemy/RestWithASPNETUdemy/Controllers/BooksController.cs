using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using RestWithASPNETUdemy.Data.VO;
using RestWithASPNETUdemy.Hypermedia.Filters;
using RestWithASPNETUdemy.Business;

namespace RestWithASPNETUdemy.Controllers
{

    /* Mapeia as requisições de http://localhost:{porta}/api/books/v1/
    Por padrão o ASP.NET Core mapeia todas as classes que extendem Controller
    pegando a primeira parte do nome da classe em lower case [Book]Controller
    e expõe como endpoint REST
    */
    [ApiVersion("1")]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class BooksController : Controller
    {
        //Declaração do serviço usado
        private IBooksBusiness _bookBusiness;

        /* Injeção de uma instancia de IBookBusiness ao criar
        uma instancia de BookController */
        public BooksController(IBooksBusiness bookBusiness)
        {
            _bookBusiness = bookBusiness;
        }

        // Configura o Swagger para a operação
        // http://localhost:{porta}/api/books/v1/
        // [ProducesResponseType((202), Type = typeof(List<Book>))]
        // determina o objeto de retorno em caso de sucesso List<Book>
        // O [ProducesResponseType(XYZ)] define os códigos de retorno 204, 400 e 401
        // Maps GET requests to https://localhost:{port}/api/book
        // Get no parameters for FindAll -> Search All
        [HttpGet("{sortDirection}/{pageSize}/{page}")]
        [ProducesResponseType((200), Type = typeof(List<BooksVO>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get(
            [FromQuery] string title,
            string sortDirection,
            int pageSize,
            int page)
        {
            return Ok(_bookBusiness.FindWithPagedSearch(title, sortDirection, pageSize, page));
        }

        // Configura o Swagger para a operação
        // http://localhost:{porta}/api/books/v1/{id}
        // [ProducesResponseType((202), Type = typeof(Book))]
        // determina o objeto de retorno em caso de sucesso Book
        // O [ProducesResponseType(XYZ)] define os códigos de retorno 204, 400 e 401
        [HttpGet("{id}")]
        [ProducesResponseType((200), Type = typeof(BooksVO))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Authorize("Bearer")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get(long id)
        {
            var book = _bookBusiness.FindByID(id);
            if (book == null) return NotFound();
            return new OkObjectResult(book);
        }

        // Configura o Swagger para a operação
        // http://localhost:{porta}/api/
        // [ProducesResponseType((202), Type = typeof(Book))]
        // determina o objeto de retorno em caso de sucesso Book
        // O [ProducesResponseType(XYZ)] define os códigos de retorno 400 e 401
        [HttpPost]
        [ProducesResponseType((201), Type = typeof(BooksVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Authorize("Bearer")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Post([FromBody] BooksVO book)
        {
            if (book == null) return BadRequest();
            return new OkObjectResult(_bookBusiness.Create(book));
        }

        // Configura o Swagger para a operação
        // http://localhost:{porta}/api/books/v1/
        // determina o objeto de retorno em caso de sucesso Book
        // O [ProducesResponseType(XYZ)] define os códigos de retorno 400 e 401
        [HttpPut]
        [ProducesResponseType((202), Type = typeof(BooksVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Authorize("Bearer")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Put([FromBody] BooksVO book)
        {
            if (book == null) return BadRequest();
            var updatedBook = _bookBusiness.Update(book);
            if (updatedBook == null) return BadRequest();
            return new OkObjectResult(updatedBook);
        }

        // Configura o Swagger para a operação
        // http://localhost:{porta}/api/books/v1/{id}
        // O [ProducesResponseType(XYZ)] define os códigos de retorno 400 e 401
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Authorize("Bearer")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Delete(int id)
        {
            _bookBusiness.Delete(id);
            return NoContent();
        }
    }
}
