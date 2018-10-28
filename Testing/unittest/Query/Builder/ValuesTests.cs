using System;
using FluentAssertions;
using VDS.RDF.Query.Builder.Assertions;
using Xunit;

namespace VDS.RDF.Query.Builder
{
    public class ValuesTests
    {
        private static readonly INodeFactory NodeFactory = new NodeFactory();

        [Fact]
        public void ShorthandMethod_SingleVariable_SingleValue_InRootGraphPattern_AddedSuccessfully()
        {
            // equivalent to
            // SELECT ?o
            // {
            //    VALUES ?o { 5 }
            // }

            // given
            var select = QueryBuilder.Select("o").GetQueryBuilder();
            select.InlineData("o").Values(5);
            var query = select.BuildQuery();

            // then
            query.RootGraphPattern.HasInlineData.Should().BeTrue();
            query.RootGraphPattern.InlineData.Should()
                .DeclareVariables("o")
                .And.HasTuples(1)
                .And.ContainTuple(new
                {
                    o = Lit(5)
                });
        }

        [Fact]
        public void ShorthandMethod_SingleVariable_MultipleValues_InRootGraphPattern_AddedSuccesfully()
        {
            // equivalent to
            // SELECT ?o
            // {
            //    VALUES ?o { 5, 10, 15 }
            // }

            // given
            var select = QueryBuilder.Select("o").GetQueryBuilder();
            select.InlineData("o")
                .Values(5)
                .Values(10)
                .Values(15);
            var query = select.BuildQuery();

            // then
            query.RootGraphPattern.HasInlineData.Should().BeTrue();
            query.RootGraphPattern.InlineData.Should()
                .DeclareVariables("o")
                .And.HasTuples(3)
                .And.ContainTuple(new { o = Lit(5) })
                .And.ContainTuple(new { o = Lit(10) })
                .And.ContainTuple(new { o = Lit(15) });
        }

        [Fact]
        public void ShorthandMethod_MultipleVariables_SingleTuple_InRootGraphPattern_AddedSuccesfully()
        {
            // equivalent to
            // SELECT *
            // {
            //    VALUES ( ?x, ?y, ?z )
            //    {
            //       ( "a", "b", "c" )
            //    }
            // }

            // given
            var select = QueryBuilder.Select("o").GetQueryBuilder();
            select.InlineData("x", "y", "z")
                .Values("a", "b", "c");
            var query = select.BuildQuery();

            // then
            query.RootGraphPattern.HasInlineData.Should().BeTrue();
            query.RootGraphPattern.InlineData.Should()
                .DeclareVariables("x", "y", "z")
                .And.HasTuples(1)
                .And.ContainTuple(new
                {
                    x = Lit("a"),
                    y = Lit("b"),
                    z = Lit("c")
                });
        }

        [Fact]
        public void ShorthandMethod_MultipleVariables_MultipleTuples_InRootGraphPattern_AddedSuccesfully()
        {
            // equivalent to
            // SELECT *
            // {
            //    VALUES ( ?x, ?y, ?z )
            //    {
            //       ( "a", "b", "c" ),
            //       ( 123, 124, 125 )
            //    }
            // }

            // given
            var select = QueryBuilder.Select("o").GetQueryBuilder();
            select.InlineData("x", "y", "z")
                .Values("a", "b", "c")
                .Values(123, 124, 125);
            var query = select.BuildQuery();

            // then
            query.RootGraphPattern.HasInlineData.Should().BeTrue();
            query.RootGraphPattern.InlineData.Should()
                .DeclareVariables("x", "y", "z")
                .And.HasTuples(2)
                .And.ContainTuple(new
                {
                    x = Lit("a"),
                    y = Lit("b"),
                    z = Lit("c")
                })
                .And.ContainTuple(new
                {
                    x = Lit(123),
                    y = Lit(124),
                    z = Lit(125)
                });
        }

        [Fact]
        public void ShorthandMethod_MultipleVariables_UriValues_InRootGraphPattern_AddedSuccesfully()
        {
            // equivalent to
            // SELECT *
            // {
            //    VALUES ( ?x, ?y, ?z )
            //    {
            //       ( <http://example.com/x>, <http://example.com/y>, <http://example.com/z> )
            //    }
            // }

            // given
            var select = QueryBuilder.SelectAll();
            select.InlineData("x", "y", "z")
                .Values(new Uri("http://example.com/x"), new Uri("http://example.com/y"), new Uri("http://example.com/z"));
            var query = select.BuildQuery();

            // then
            query.RootGraphPattern.HasInlineData.Should().BeTrue();
            query.RootGraphPattern.InlineData.Should()
                .DeclareVariables("x", "y", "z")
                .And.HasTuples(1)
                .And.ContainTuple(new
                {
                    x = Uri("http://example.com/x"),
                    y = Uri("http://example.com/y"),
                    z = Uri("http://example.com/z"),
                });
        }

        [Fact]
        public void ShorthandMethod_MultipleVariables_MixedValuesWithUndef_InRootGraphPattern_AddedSuccesfully()
        {
            // equivalent to
            // SELECT *
            // {
            //    VALUES ( ?x, ?y, ?z )
            //    {
            //       ( "Tomasz", <http://example.com/y>, UNDEF ),
            //       ( "Tomasz", UNDEF, 6 )
            //    }
            // }

            // given
            var select = QueryBuilder.SelectAll();
            select.InlineData("x", "y", "z")
                .Values("Tomasz", new Uri("http://example.com/y"), null)
                .Values("Rob", null, 6);
            var query = select.BuildQuery();

            // then
            query.RootGraphPattern.HasInlineData.Should().BeTrue();
            query.RootGraphPattern.InlineData.Should()
                .DeclareVariables("x", "y", "z")
                .And.HasTuples(2)
                .And.ContainTuple(new
                {
                    x = Lit("Tomasz"),
                    y = Uri("http://example.com/y"),
                    z = UNDEF,
                })
                .And.ContainTuple(new
                {
                    x = Lit("Rob"),
                    y = UNDEF,
                    z = Lit(6),
                });
        }

        [Fact]
        public void VerboseMethod_SingleVariable_MultipleValues_InRootGraphPattern_AddedSuccesfully()
        {
            // equivalent to
            // SELECT *
            // {
            //    VALUES ?x
            //    {
            //       ( "Tomasz" )
            //       ( 20 )
            //       ( <http://example.com> )
            //    }
            // }

            // given
            var select = QueryBuilder.SelectAll();
            select.InlineData("x")
                .Values(vb => vb.Value("Tomasz"))
                .Values(vb => vb.Value(20))
                .Values(vb => vb.Value(new Uri("http://example.com")));
            var query = select.BuildQuery();

            // then
            query.RootGraphPattern.HasInlineData.Should().BeTrue();
            query.RootGraphPattern.InlineData.Should()
                .DeclareVariables("x")
                .And.HasTuples(3)
                .And.ContainTuple(new
                {
                    x = Lit("Tomasz"),
                })
                .And.ContainTuple(new
                {
                    x = Lit(20),
                })
                .And.ContainTuple(new
                {
                    x = Uri("http://example.com"),
                });
        }

        [Fact]
        public void VerboseMethod_UndefValues_InRootGraphPattern_AddedSuccesfully()
        {
            // equivalent to
            // SELECT *
            // {
            //    VALUES ?x ?y ?z
            //    {
            //       ( UNDEF UNDEF UNDEF )
            //    }
            // }

            // given
            var select = QueryBuilder.SelectAll();
            select.InlineData("x", "y", "z")
                .Values(vb => vb.Undef().Undef().Undef());
            var query = select.BuildQuery();

            // then
            query.RootGraphPattern.HasInlineData.Should().BeTrue();
            query.RootGraphPattern.InlineData.Should()
                .DeclareVariables("x", "y", "z")
                .And.HasTuples(1)
                .And.ContainTuple(new
                {
                    x = UNDEF,
                    y = UNDEF,
                    z = UNDEF
                });
        }

        [Fact]
        public void VerboseMethod_TypedLiteral_InRootGraphPattern_AddedSuccesfully()
        {
            // equivalent to
            // SELECT *
            // {
            //    VALUES ?name
            //    {
            //       ( "Tomasz"^^<https://schema.org/givenName> )
            //    }
            // }

            // given
            var select = QueryBuilder.SelectAll();
            select.InlineData("name")
                .Values(vb => vb.Value("Tomasz", new Uri("https://schema.org/givenName")));
            var query = select.BuildQuery();

            // then
            query.RootGraphPattern.HasInlineData.Should().BeTrue();
            query.RootGraphPattern.InlineData.Should()
                .DeclareVariables("name")
                .And.HasTuples(1)
                .And.ContainTuple(new
                {
                    name = Lit("Tomasz", new Uri("https://schema.org/givenName"))
                });
        }

        [Fact]
        public void VerboseMethod_TaggedLiteral_InRootGraphPattern_AddedSuccesfully()
        {
            // equivalent to
            // SELECT *
            // {
            //    VALUES ?name
            //    {
            //       ( "Tomasz"@pl )
            //       ( "Thomas"@en )
            //    }
            // }

            // given
            var select = QueryBuilder.SelectAll();
            select.InlineData("name")
                .Values(vb => vb.Value("Tomasz", "pl"))
                .Values(vb => vb.Value("Thomas", "en"));
            var query = select.BuildQuery();

            // then
            query.RootGraphPattern.HasInlineData.Should().BeTrue();
            query.RootGraphPattern.InlineData.Should()
                .DeclareVariables("name")
                .And.HasTuples(2)
                .And.ContainTuple(new
                {
                    name = Lit("Tomasz", "pl")
                })
                .And.ContainTuple(new
                {
                    name = Lit("Thomas", "en")
                });
        }

        [Fact]
        public void ShorthandMethod_OverEntireQuery_AddedSuccesfully()
        {
            // equivalent to
            // SELECT *
            // {
            // }
            // VALUES ?x ?y ?z
            // {
            //    ( 10 "Hello" <http://some.url> )
            // }

            // given
            var select = QueryBuilder.SelectAll();
            select.InlineDataOverQuery("x", "y", "z")
                .Values(10, "Hello", new Uri("http://some.url"));
            var query = select.BuildQuery();

            // then
            query.Bindings.Should()
                .DeclareVariables("x", "y", "z")
                .And.HasTuples(1)
                .And.ContainTuple(new
                {
                    x = Lit(10),
                    y = Lit("Hello"),
                    z = Uri("http://some.url")
                });
        }

        private static IUriNode Uri(string uri)
        {
            return NodeFactory.CreateUriNode(new Uri(uri));
        }

        private static ILiteralNode Lit<T>(T literal)
        {
            return NodeFactory.CreateLiteralNode(literal.ToString());
        }

        private static ILiteralNode Lit(object literal, Uri type)
        {
            return NodeFactory.CreateLiteralNode(literal.ToString(), type);
        }

        private static ILiteralNode Lit(object literal, string langugeTag)
        {
            return NodeFactory.CreateLiteralNode(literal.ToString(), langugeTag);
        }

        private static INode UNDEF => null;
    }
}