﻿using MediatR;

namespace BuildingBlocks.Application;

public interface IQuery<out TResult> : IRequest<TResult>
{
}