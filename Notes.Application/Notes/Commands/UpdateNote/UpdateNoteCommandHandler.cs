using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Application.Common.Exceptions;
using Notes.Application.Interfaces;
using Notes.Domain;

namespace Notes.Application.Notes.Commands.UpdateNote
{
    public class UpdateNoteCommandHandler : IRequestHandler<UpdateNoteCommand>
    {
        private readonly INotesDbContext _db;

        public UpdateNoteCommandHandler(INotesDbContext db) =>
            _db = db;

        public async Task<Unit> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
        {
            var entity = await _db.Notes.FirstOrDefaultAsync(note => note.Id == request.Id, cancellationToken);
            if (entity is null || entity.UserId != request.Id)
                throw new NotFoundException(nameof(Note), request.Id);
            
            entity.Details = request.Details;
            entity.Title = request.Title;
            entity.EditDateTime = DateTime.Now;
            await _db.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}