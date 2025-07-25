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
using System.Collections.Generic;
using System.Linq;
using VDS.RDF.Query.Patterns;

namespace VDS.RDF.Query.Builder;

/// <summary>
/// Provides methods for building triple patterns.
/// </summary>
public class TriplePatternBuilder : ITriplePatternBuilderInternal
{
    private readonly IList<ITriplePattern> _patterns = new List<ITriplePattern>();
    private readonly PatternItemFactory _patternItemFactory;

    /// <summary>
    /// Gets the triple patterns.
    /// </summary>
    public ITriplePattern[] Patterns => _patterns.ToArray();

    /// <summary>
    /// Gets the pattern item factory.
    /// </summary>
    public IPatternItemFactory PatternItemFactory => _patternItemFactory;

    /// <inheritdoc/>
    public INamespaceMapper Prefixes { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="prefixes"></param>
    public TriplePatternBuilder(INamespaceMapper prefixes)
    {
        Prefixes = prefixes;
        _patternItemFactory = new PatternItemFactory();
    }

    /// <inheritdoc />
    public TriplePatternPredicatePart Subject(SparqlVariable subjectVariable)
    {
        return Subject(PatternItemFactory.CreateVariablePattern(subjectVariable.Name));
    }

    /// <inheritdoc />
    public TriplePatternPredicatePart Subject(string subjectVariableName)
    {
        return Subject(PatternItemFactory.CreateVariablePattern(subjectVariableName));
    }

    /// <inheritdoc/>
    public TriplePatternPredicatePart Subject<TNode>(string subject) where TNode : INode
    {
        return Subject(_patternItemFactory.CreatePatternItem(typeof(TNode), subject, Prefixes));
    }

    /// <inheritdoc/>
    public TriplePatternPredicatePart Subject(INode subjectNode)
    {
        return Subject(PatternItemFactory.CreateNodeMatchPattern(subjectNode));
    }

    /// <inheritdoc/>
    public TriplePatternPredicatePart Subject(Uri subject)
    {
        return Subject(PatternItemFactory.CreateNodeMatchPattern(subject));
    }

    /// <inheritdoc/>
    public TriplePatternPredicatePart Subject(PatternItem subject)
    {
        return new TriplePatternPredicatePart(this, subject, Prefixes);
    }

    /// <inheritdoc />
    public void AddPattern(TriplePattern triplePattern)
    {
        _patterns.Add(triplePattern);
    }
}