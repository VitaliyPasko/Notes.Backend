using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Notes.Application.Interfaces;
using Notes.Domain;

namespace Notes.Application.Notes.Commands.CreateNote
{
    public class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, Guid>
    {
        private readonly INotesDbContext _db;

        public CreateNoteCommandHandler(INotesDbContext dbContext) => _db = dbContext;

        public async Task<Guid> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
        {
            var note = new Note
            {
                UserId = request.UserId,
                Title = request.Title,
                Details = request.Details,
                Id = Guid.NewGuid(),
                CreationDateTime = DateTime.Now,
                EditDateTime = null
            };

            await _db.Notes.AddAsync(note, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
            
            return note.Id;
        }
    }
}