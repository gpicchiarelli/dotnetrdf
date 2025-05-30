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

using VDS.RDF.Query.Optimisation;

namespace VDS.RDF.Query.Algebra;

/// <summary>
/// Represents an Algebra construct which is an Abstract Join (i.e. any kind of Join over two algebra operators).
/// </summary>
/// <remarks>
/// Specific sub-interfaces are used to mark specific kinds of Join.
/// </remarks>
public interface IAbstractJoin
    : ISparqlAlgebra
{
    /// <summary>
    /// Gets the LHS of the Join.
    /// </summary>
    ISparqlAlgebra Lhs
    {
        get;
    }

    /// <summary>
    /// Gets the RHS of the Join.
    /// </summary>
    ISparqlAlgebra Rhs
    {
        get;
    }

    /// <summary>
    /// Transforms both sides of the Join using the given Optimiser.
    /// </summary>
    /// <param name="optimiser">Optimser.</param>
    /// <returns></returns>
    /// <remarks>
    /// The join should retain all it's existing properties and just return a new version of itself with the two sides of the join having had the given optimiser applied to them.
    /// </remarks>
    ISparqlAlgebra Transform(IAlgebraOptimiser optimiser);

    /// <summary>
    /// Transforms the LHS of the Join using the given Optimiser.
    /// </summary>
    /// <param name="optimiser">Optimser.</param>
    /// <returns></returns>
    /// <remarks>
    /// The join should retain all it's existing properties and just return a new version of itself with LHS side of the join having had the given optimiser applied to them.
    /// </remarks>
    ISparqlAlgebra TransformLhs(IAlgebraOptimiser optimiser);

    /// <summary>
    /// Transforms the RHS of the Join using the given Optimiser.
    /// </summary>
    /// <param name="optimiser">Optimser.</param>
    /// <returns></returns>
    /// <remarks>
    /// The join should retain all it's existing properties and just return a new version of itself with RHS side of the join having had the given optimiser applied to them.
    /// </remarks>
    ISparqlAlgebra TransformRhs(IAlgebraOptimiser optimiser);
}