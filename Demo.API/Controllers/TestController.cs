using Demo.API.CQRS;
using Demo.API.Domain.Entities.Catalog;
using Demo.API.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.API.Controllers
{
    public class Dick
    {
        public int Length { get; set; }
    }

    public class DickModel
    {
        public int Length { get; set; }
    }

    public class GetSomethingQuery : IQuery<DickModel>
    {
        public string Name { get; set; }
    }

    public class GetSomethingQueryHandler : IQueryHandler<GetSomethingQuery, DickModel>
    {
        public Task<DickModel> Handle(GetSomethingQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new DickModel { Length = 99 });
        }
    }

    public class DoSomethingCmd : IResultCommand
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IRepository<Category> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public TestController(IMediator mediator, IRepository<Category> repository, IUnitOfWork unitOfWork)
        {
            _mediator = mediator;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Get(GetSomethingQuery query)
        {
            var res = await _mediator.Send(query);
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> Post(DoSomethingCmd comd)
        {
            _repository.Add(new Category { Name = "Clothes " });
            await _unitOfWork.SaveChangesAsync();
            var cats = await _repository.GetAllAsync();
            return Ok(new { comd,  cats });
        }
    }
}
