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

namespace VDS.RDF.Query.Expressions.Primary;

/// <summary>
/// Class representing Variable value expressions.
/// </summary>
public class VariableTerm
    : ISparqlExpression
{
    /// <summary>
    /// Creates a new Variable Expression.
    /// </summary>
    /// <param name="name">Variable Name.</param>
    public VariableTerm(string name)
    {
        Name = name;

        // Strip leading ?/$ if present
        if (Name.StartsWith("?") || Name.StartsWith("$"))
        {
            Name = Name.Substring(1);
        }
    }
    
    /// <summary>
    /// Get the variable name.
    /// </summary>
    public string Name { get; }


    /// <summary>
    /// Gets the String representation of this Expression.
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return "?" + Name;
    }

    /// <inheritdoc />
    public TResult Accept<TResult, TContext, TBinding>(ISparqlExpressionProcessor<TResult, TContext, TBinding> processor, TContext context, TBinding binding)
    {
        return processor.ProcessVariableTerm(this, context, binding);
    }

    /// <inheritdoc />
    public T Accept<T>(ISparqlExpressionVisitor<T> visitor)
    {
        return visitor.VisitVariableTerm(this);
    }

    /// <summary>
    /// Gets the enumeration containing the single variable that this expression term represents.
    /// </summary>
    public IEnumerable<string> Variables
    {
        get
        {
            return Name.AsEnumerable();
        }
    }

    /// <summary>
    /// Gets the Type of the Expression.
    /// </summary>
    public SparqlExpressionType Type
    {
        get
        {
            return SparqlExpressionType.Primary;
        }
    }

    /// <summary>
    /// Gets the Functor of the Expression.
    /// </summary>
    public string Functor
    {
        get
        {
            return string.Empty;
        }
    }

    /// <summary>
    /// Gets the Arguments of the Expression.
    /// </summary>
    public IEnumerable<ISparqlExpression> Arguments
    {
        get
        {
            return Enumerable.Empty<ISparqlExpression>();
        }
    }

    /// <summary>
    /// Gets whether an expression can safely be evaluated in parallel.
    /// </summary>
    public virtual bool CanParallelise
    {
        get
        {
            return true;
        }
    }

    /// <summary>
    /// Transforms the Expression using the given Transformer.
    /// </summary>
    /// <param name="transformer">Expression Transformer.</param>
    /// <returns></returns>
    public ISparqlExpression Transform(IExpressionTransformer transformer)
    {
        return this;
    }
}
