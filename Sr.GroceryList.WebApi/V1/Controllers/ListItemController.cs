using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Sr.GroceryList.Dal;
using Sr.GroceryList.Entities;
using Sr.GroceryList.WebApi.V1.Models;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Profiling;
using Microsoft.AspNetCore.Http;

namespace Sr.GroceryList.WebApi.V1.Controllers
{
    [Produces("application/json")]
    [ApiVersion("1.0", Deprecated = false)]
    //[Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ListItemController : ControllerBase
    {
        private readonly IListItemRepository _ListItemRepository;
        protected IMapper _Mapper = null;

        public ListItemController(IListItemRepository ListItemRepository)
        {
            _ListItemRepository = ListItemRepository;

            // ToDo SR : Maybe use DI to inject the mapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ListItemDto, ListItem>();
                cfg.CreateMap<ListItem, ListItemDto>();
            });
            _Mapper = config.CreateMapper();
        }

        // GET: api/ListItem
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<IEnumerable<ListItem>>> GetListItems()
        {
            //ToDo SR create method attribute for MiniProfiler
            using (MiniProfiler.Current.Step("GetListItems"))
            {
                var dtos = await _ListItemRepository.GetAllAsync();

                var result = _Mapper.Map<IEnumerable<ListItemDto>, List<ListItem>>(dtos);
                return this.Ok(result);
            }
        }

        // GET: api/ListItem/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<ListItem>> GetListItem(int id)
        {
            var ListItem = await _ListItemRepository.GetByIdAsync(id);

            if (ListItem == null)
            {
                Response.Headers.Add("x-status-reason", $"ListItem with id '{id}' is not found.");
                return NotFound();
            }

            var result = _Mapper.Map<ListItemDto, ListItem>(ListItem);
            return this.Ok(result);
        }

        // POST: api/ListItem
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<ListItem>> PostListItem(ListItem item)
        {
            if (item.Id > 0)
            {
                Response.Headers.Add("x-status-reason", $"ListItem should not have an non positive id '{item.Id}' ");
                return BadRequest();
            }
            var dto = _Mapper.Map<ListItem, ListItemDto>(item);

            var inserted = await _ListItemRepository.InsertAsync(dto);
            if (inserted.Id <= 0)
            {
                Response.Headers.Add("x-status-reason", $"ListItem '{item}' is not saved.");
                this.BadRequest();
            }

            var result = _Mapper.Map<ListItemDto, ListItem>(inserted);
            return this.Ok(result);
            //return CreatedAtAction(Codeof(GetListItem), new { id = item.Id }, item);
        }

        // PUT: api/ListItem/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<bool>> PutListItem(int id, ListItem item)
        {
            if (id != item.Id)
            {
                Response.Headers.Add("x-status-reason", $"ListItem with id '{item.Id}' does not match the id '{id}'.");
                return BadRequest();
            }
            var dto = _Mapper.Map<ListItem, ListItemDto>(item);			

            var updated = await _ListItemRepository.UpdateAsync(dto);
            if (!updated)
            {
                Response.Headers.Add("x-status-reason", $"ListItem '{item}' is not saved.");
                this.BadRequest();
            }

            return this.Ok(updated);
        }

        // DELETE: api/ListItem/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<bool>> DeleteListItem(int id)
        {
            var ListItem = await _ListItemRepository.GetByIdAsync(id);

            if (ListItem == null)
            {
                Response.Headers.Add("x-status-reason", $"ListItem with id '{id}' is not found.");
                return NotFound();
            }

            var result = await _ListItemRepository.DeleteAsync(ListItem);
            if (!result)
            {
                Response.Headers.Add("x-status-reason", $"ListItem '{ListItem}' is not deleted.");
                this.BadRequest();
            }
            return this.Ok(result);
        }
    }
}
