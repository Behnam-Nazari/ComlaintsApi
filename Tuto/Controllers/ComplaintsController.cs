using AutoMapper;
using Data.Contracts;
using Data.Repositories;
using Entity.Complaitns;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Dtos;
using System.Threading;

namespace Tuto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplaintsController : ControllerBase
    {
        private readonly IComplaintsRepository _complaitnsRepository;
        private readonly IMapper _Mapper;

        public ComplaintsController(IComplaintsRepository complaitnsRepository, IMapper mapper)
        {
            _complaitnsRepository = complaitnsRepository;
            _Mapper = mapper;
        }

        [HttpGet]
        public async Task<List<Complaints>> Get()
        {
            var complaints = await _complaitnsRepository.TableNoTracking.ToListAsync();
            return complaints;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Complaints>> Get(CancellationToken cancellationToken, int id)
        {
            var complaint = await _complaitnsRepository.GetByIdAsync(cancellationToken ,id);
            return Ok(complaint);
        }

        [HttpPost]
        public async Task<ActionResult<Complaints>> Create(ComplaintsDto complaintDto, CancellationToken cancellationToken)
        {
            //var complaint = new Complaints
            //{
            //    FirstName = complaintDto.FirstName,
            //    LastName = complaintDto.LastName,
            //    PhoneNumber = complaintDto.PhoneNumber,
            //    Email = complaintDto.Email,
            //    Message = complaintDto.Message

            //};
            var complaint = _Mapper.Map<Complaints>(complaintDto);
            await _complaitnsRepository.AddAsync(complaint, cancellationToken);
            return Ok(complaint);
        }

        //[HttpPost]
        //public async Task<ActionResult<ComplaintsDto>> CreateRange(IEnumerable<Complaints> complaints, CancellationToken cancellationToken)
        //{
        //    await _complaitnsRepository.AddRangeAsync(complaints, cancellationToken);
        //    return Ok(complaints);
        //}

        [HttpPut]
        public async Task<ActionResult<Complaints>> Update(int id, ComplaintsDto complaintDto, CancellationToken cancellationToken)
        {
            var complaint = await _complaitnsRepository.GetByIdAsync(cancellationToken, id);
            //initialComplaint.FirstName = complaintDto.FirstName;
            //initialComplaint.LastName = complaintDto.LastName;
            //initialComplaint.Email = complaintDto.Email;
            //initialComplaint.PhoneNumber = complaintDto.PhoneNumber;
            //initialComplaint.Message = complaintDto.Message;
            //initialComplaint.ModifiedOn = DateTime.UtcNow;
            _Mapper.Map(complaintDto, complaint);
            complaint.ModifiedOn = DateTime.UtcNow;
            await _complaitnsRepository.UpdateAsyc(complaint, cancellationToken);
            return Ok(complaint);
            
        }

        //[HttpPost]
        //public async Task<ActionResult<Complaints>> AddPhoto()

        [HttpDelete]
        public async Task<ActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var entity = _complaitnsRepository.GetByIdAsync(cancellationToken, id);

            await _complaitnsRepository.DeleteAsync(await entity.ConfigureAwait(false), cancellationToken);
            return Ok();
        }

    }
}
