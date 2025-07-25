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

namespace VDS.RDF;

/// <summary>
/// Abstract decorator for Graph Collections to make it easier to add new functionality to existing implementations.
/// </summary>
public abstract class WrapperGraphCollection
    : BaseGraphCollection
{
    /// <summary>
    /// Underlying Graph Collection.
    /// </summary>
    protected readonly BaseGraphCollection _graphs;

    /// <summary>
    /// Creates a decorator around a default <see cref="GraphCollection"/> instance.
    /// </summary>
    protected WrapperGraphCollection()
        : this(new GraphCollection()) { }

    /// <summary>
    /// Creates a decorator around the given graph collection.
    /// </summary>
    /// <param name="graphCollection">Graph Collection.</param>
    protected WrapperGraphCollection(BaseGraphCollection graphCollection)
    {
        _graphs = graphCollection ?? throw new ArgumentNullException(nameof(graphCollection));
        _graphs.GraphAdded += HandleGraphAdded;
        _graphs.GraphRemoved += HandleGraphRemoved;
    }

    private void HandleGraphAdded(object sender, GraphEventArgs args)
    {
        RaiseGraphAdded(args.Graph);
    }

    private void HandleGraphRemoved(object sender, GraphEventArgs args)
    {
        RaiseGraphRemoved(args.Graph);
    }

    /// <summary>
    /// Adds a Graph to the collection.
    /// </summary>
    /// <param name="g">Graph.</param>
    /// <param name="mergeIfExists">Whether to merge into an existing Graph with the same URI.</param>
    /// <returns></returns>
    public override bool Add(IGraph g, bool mergeIfExists)
    {
        return _graphs.Add(g, mergeIfExists);
    }

    /// <summary>
    /// Gets whether the collection contains the given Graph.
    /// </summary>
    /// <param name="graphUri"></param>
    /// <returns></returns>
    [Obsolete("Replaced by Contains(IRefNode)")]
    public override bool Contains(Uri graphUri)
    {
        return _graphs.Contains(graphUri);
    }

    /// <summary>
    /// Checks whether the graph with the given name exists in this graph collection.
    /// </summary>
    /// <param name="graphName">Graph name to test for.</param>
    /// <returns>True if a graph with the specified name is in the collection, false otherwise.</returns>
    /// <remarks>The null value is used to reference the default (unnamed) graph.</remarks>
    public override bool Contains(IRefNode graphName)
    {
        return _graphs.Contains(graphName);
    }

    /// <summary>
    /// Gets the number of Graphs in the collection.
    /// </summary>
    public override int Count
    {
        get
        {
            return _graphs.Count;
        }
    }

    /// <summary>
    /// Disposes of the collection.
    /// </summary>
    public override void Dispose()
    {
        _graphs.Dispose();
    }

    /// <summary>
    /// Gets the enumerator for the collection.
    /// </summary>
    /// <returns></returns>
    public override IEnumerator<IGraph> GetEnumerator()
    {
        return _graphs.GetEnumerator();
    }

    /// <summary>
    /// Gets the URIs of the Graphs in the collection.
    /// </summary>
    [Obsolete("Replaced by GraphNames")]
    public override IEnumerable<Uri> GraphUris
    {
        get 
        {
            return _graphs.GraphUris;
        }
    }

    /// <summary>
    /// Provides an enumeration of the names of all of teh graphs in the collection.
    /// </summary>
    public override IEnumerable<IRefNode> GraphNames => _graphs.GraphNames;

    /// <summary>
    /// Removes a Graph from the collection.
    /// </summary>
    /// <param name="graphUri">Graph URI.</param>
    /// <returns></returns>
    [Obsolete("Replaced by Remove(IRefNode)")]
    public override bool Remove(Uri graphUri)
    {
        return _graphs.Remove(graphUri);
    }

    /// <summary>
    /// Removes a graph from the collection.
    /// </summary>
    /// <param name="graphName">Name of the Graph to remove.</param>
    /// <remarks>
    /// The null value is used to reference the Default Graph.
    /// </remarks>
    public override bool Remove(IRefNode graphName)
    {
        return _graphs.Remove(graphName);
    }

    /// <summary>
    /// Gets a Graph from the collection.
    /// </summary>
    /// <param name="graphUri">Graph URI.</param>
    /// <returns></returns>
    [Obsolete("Replaced by this[IRefNode]")]
    public override IGraph this[Uri graphUri]
    {
        get
        {
            return _graphs[graphUri];
        }
    }

    /// <summary>
    /// Gets a graph from the collection.
    /// </summary>
    /// <param name="graphName">The name of the graph to retrieve.</param>
    /// <returns></returns>
    /// <remarks>The null value is used to reference the default graph.</remarks>
    public override IGraph this[IRefNode graphName] => _graphs[graphName];
}
