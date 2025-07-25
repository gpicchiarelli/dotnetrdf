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

namespace VDS.RDF.Query.Spin.Model;


/**
 * Shared base class for Argument and Attribute.
 * 
 * @author Holger Knublauch
 */
internal interface IAbstractAttribute : IResource
{

    /**
     * Gets the description (stored in rdfs:comment) of this.
     * @return the description (if any exists)
     */
    String getComment();

    /**
     * Gets the specified sp:argProperty (if any).
     * @return the argProperty
     */
    IResource getPredicate();

    /**
     * Gets the specified spl:valueType (if any).
     * @return the value type
     */
    IResource getValueType();


    /**
     * Checks if this argument has been declared to be optional.
     * For Arguments this means spl:optional = true.
     * For Attributes this means spl:minCardinality = 0
     * @return  true if optional
     */
    bool IsOptional();
}