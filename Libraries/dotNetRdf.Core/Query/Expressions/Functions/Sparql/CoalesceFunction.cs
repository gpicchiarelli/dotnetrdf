/*
// <copyright>
// dotNetRDF is free and open source software licensed under the MIT License
// -------------------------------------------------------------------------
// 
// Copyright (c) 2009-2025 dotNetRDF Project (http://dotnetrdf.org/)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is furnished
// to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
// CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// </copyright>
*/

using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VDS.RDF.Query.Expressions.Functions.Sparql;

/// <summary>
/// Class representing the SPARQL COALESCE() function.
/// </summary>
public class CoalesceFunction 
    : ISparqlExpression
{
    private readonly List<ISparqlExpression> _expressions = new();

    /// <summary>
    /// Creates a new COALESCE function with the given expressions as its arguments.
    /// </summary>
    /// <param name="expressions">Argument expressions.</param>
    public CoalesceFunction(IEnumerable<ISparqlExpression> expressions)
    {
        _expressions.AddRange(expressions);
    }

    /// <summary>
    /// Get the list of expressions to coalesce over.
    /// </summary>
    public IEnumerable<ISparqlExpression> InnerExpressions { get => _expressions; }

    /// <inheritdoc />
    public TResult Accept<TResult, TContext, TBinding>(ISparqlExpressionProcessor<TResult, TContext, TBinding> processor, TContext context, TBinding binding)
    {
        return processor.ProcessCoalesceFunction(this, context, binding);
    }

    /// <inheritdoc />
    public T Accept<T>(ISparqlExpressionVisitor<T> visitor)
    {
        return visitor.VisitCoalesceFunction(this);
    }

    /// <summary>
    /// Gets the Variables used in all the argument expressions of this function.
    /// </summary>
    public IEnumerable<string> Variables
    {
        get 
        {
            return (from e in _expressions
                    from v in e.Variables
                    select v);
        }
    }

    /// <summary>
    /// Gets the String representation of the function.
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        var output = new StringBuilder();
        output.Append("COALESCE(");
        for (var i = 0; i < _expressions.Count; i++)
        {
            output.Append(_expressions[i]);
            if (i < _expressions.Count - 1)
            {
                output.Append(", ");
            }
        }
        output.Append(")");
        return output.ToString();
    }

    /// <summary>
    /// Gets the Type of the Expression.
    /// </summary>
    public SparqlExpressionType Type
    {
        get
        {
            return SparqlExpressionType.Function;
        }
    }

    /// <summary>
    /// Gets the Functor of the Expression.
    /// </summary>
    public string Functor
    {
        get
        {
            return SparqlSpecsHelper.SparqlKeywordCoalesce;
        }
    }

    /// <summary>
    /// Gets the Arguments of the Expression.
    /// </summary>
    public IEnumerable<ISparqlExpression> Arguments
    {
        get
        {
            return _expressions;
        }
    }

    /// <summary>
    /// Gets whether an expression can safely be evaluated in parallel.
    /// </summary>
    public virtual bool CanParallelise
    {
        get
        {
            return _expressions.All(e => e.CanParallelise);
        }
    }

    /// <summary>
    /// Transforms the Expression using the given Transformer.
    /// </summary>
    /// <param name="transformer">Expression Transformer.</param>
    /// <returns></returns>
    public ISparqlExpression Transform(IExpressionTransformer transformer)
    {
        return new CoalesceFunction(_expressions.Select(e => transformer.Transform(e)));
    }
}
