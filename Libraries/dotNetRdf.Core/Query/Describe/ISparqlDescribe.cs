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

using System;

namespace VDS.RDF.Query.Describe;

/// <summary>
/// Interface for classes that implement the DESCRIBE functionality of SPARQL.
/// </summary>
/// <remarks>
/// <para>
/// This is designed so that developers can introduce their own DESCRIBE algorithms as required.
/// </para>
/// </remarks>
public interface ISparqlDescribe
{
    /// <summary>
    /// Generates a Graph which is the description of the resources resulting from the Query.
    /// </summary>
    /// <param name="context">SPARQL Evaluation Context.</param>
    /// <returns></returns>
    [Obsolete("Replaced by Describe(ISparqlDescribeContext).")]
    IGraph Describe(SparqlEvaluationContext context);

    /// <summary>
    /// Generates the Description Graph based on the Query Results from the given Evaluation Context passing the resulting Triples to the given RDF Handler.
    /// </summary>
    /// <param name="handler">RDF Handler.</param>
    /// <param name="context">SPARQL Evaluation Context.</param>
    [Obsolete("Replaced by Describe(IRdfHandler, ISparqlDescribeContext).")]
    void Describe(IRdfHandler handler, SparqlEvaluationContext context);
    
    /// <summary>
    /// Generates a Graph which is the description of the resources resulting from the Query.
    /// </summary>
    /// <param name="context">SPARQL Describe evaluation context.</param>
    /// <returns></returns>
    IGraph Describe(ISparqlDescribeContext context);

    /// <summary>
    /// Generates the Description Graph based on the Query Results from the given context passing the resulting Triples to the given RDF Handler.
    /// </summary>
    /// <param name="handler">RDF Handler.</param>
    /// <param name="context">SPARQL Describe evaluation context.</param>
    void Describe(IRdfHandler handler, ISparqlDescribeContext context);
}
