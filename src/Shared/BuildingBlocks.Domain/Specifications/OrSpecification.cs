﻿using System.Linq.Expressions;

namespace BuildingBlocks.Domain.Specifications;

public class OrSpecification<T> : Specification<T>
{
    private readonly Specification<T> _leftSpecification;
    private readonly Specification<T> _rightSpecification;

    public OrSpecification(Specification<T> leftSpecification, Specification<T> rightSpecification)
    {
        _leftSpecification = leftSpecification;
        _rightSpecification = rightSpecification;
    }

    public override Expression<Func<T, bool>> ToExpression()
    {
        var leftExpression = _leftSpecification.ToExpression();
        var rightExpression = _rightSpecification.ToExpression();

        var rebindParameterVisitor = new ParameterReplacer(rightExpression.Parameters[0], leftExpression.Parameters[0]);
        var right = rebindParameterVisitor.Visit(rightExpression.Body);
        var expression = Expression.OrElse(leftExpression.Body, right ?? throw new InvalidOperationException());
        return Expression.Lambda<Func<T, bool>>(expression, leftExpression.Parameters);
    }
}