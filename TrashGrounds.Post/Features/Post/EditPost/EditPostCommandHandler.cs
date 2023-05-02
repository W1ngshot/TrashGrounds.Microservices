﻿using MediatR;
using TrashGrounds.Post.Database.Postgres;
using TrashGrounds.Post.Infrastructure;
using TrashGrounds.Post.Infrastructure.Exceptions;

namespace TrashGrounds.Post.Features.Post.EditPost;

public class EditPostCommandHandler : IRequestHandler<EditPostCommand, Models.Main.Post>
{
    private readonly PostDbContext _context;

    public EditPostCommandHandler(PostDbContext context)
    {
        _context = context;
    }

    public async Task<Models.Main.Post> Handle(EditPostCommand request, CancellationToken cancellationToken)
    {
        var post = await _context.Posts.FirstOrNotFoundAsync(post => post.Id == request.PostId,
            cancellationToken: cancellationToken);

        if (post.UserId != request.CurrentUserId)
            throw new ForbiddenException("Not your post");

        post.AssetLink = request.AssetLink; //переделать на вызов file microservice
        
        post.Text = request.Text;
        post.IsHidden = request.IsHidden;

        await _context.SaveEntitiesAsync();

        return post;
    }
}