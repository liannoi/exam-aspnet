using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Exam.Application.Common.Interfaces;
using Exam.Application.Storage.Voting;
using Exam.Domain.Entities;
using MediatR;

namespace Exam.Application.Storage.VotingPolles.Commands.Create
{
    public class CreateVotingPolleCommand : IRequest
    {
        public IEnumerable<VotingAnswerLookupDto> Answers { get; set; }

        public class CreateVotingPolleCommandHandler : IRequestHandler<CreateVotingPolleCommand>
        {
            private readonly IFilmsDbContext _context;
            private readonly IMapper _mapper;

            public CreateVotingPolleCommandHandler(IFilmsDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(CreateVotingPolleCommand request, CancellationToken cancellationToken)
            {
                foreach (var answer in request.Answers)
                    await _context.VotingPolles.AddAsync(new VotingPolleRelation
                            {PolleId = 1, VotingAnswerId = answer.VotingAnswerId, VotingId = answer.VotingId},
                        cancellationToken);

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}