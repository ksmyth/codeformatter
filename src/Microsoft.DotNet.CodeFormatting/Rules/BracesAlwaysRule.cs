// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.DotNet.CodeFormatting.Rules
{
    [SyntaxRule(BraceNewLineRule.Name, BraceNewLineRule.Description, SyntaxRuleOrder.BraceNewLineRule)]
    internal sealed class BracesAlwaysRule : CSharpOnlyFormattingRule, ISyntaxFormattingRule
    {
        internal const string Name = "BracesAlwaysRule";
        internal const string Description = "BracesAlwaysRule";

        private enum NewLineKind
        {
            WhitespaceAndNewLine,
            NewLine,
            Directive,
            None,
        }

        public SyntaxNode Process(SyntaxNode syntaxNode, string languageName)
        {
            // awful kludge
            for (int i = 0; i < 200; i++)

                foreach (var node in syntaxNode.DescendantNodes())
                {
                    if (node.Kind() == SyntaxKind.IfStatement)
                    {
                        var if_ = (IfStatementSyntax)node;

                        if (if_.Statement.GetType() != typeof(BlockSyntax))
                        {
                            Console.Out.WriteLine(" if XXX " + if_.Statement.GetType().Name);
                            syntaxNode = syntaxNode.ReplaceNode(if_.Statement, SyntaxFactory.Block(if_.Statement));
                            //syntaxNode = syntaxNode.ReplaceNode(if_.Statement, SyntaxFactory.EmptyStatement());
                        }
                        if (if_.Else != null && if_.Else.Statement.GetType() != typeof(BlockSyntax) && if_.Else.Statement.GetType() != typeof(IfStatementSyntax))
                        {
                            Console.Out.WriteLine(" else XXX " + if_.Else.Statement.GetType().Name);
                            syntaxNode = syntaxNode.ReplaceNode(if_.Else.Statement, SyntaxFactory.Block(if_.Else.Statement));
                        }

                    }
                }
            return syntaxNode;
        }
    }
}
