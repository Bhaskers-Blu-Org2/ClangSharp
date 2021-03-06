// Copyright (c) Microsoft and Contributors. All rights reserved. Licensed under the University of Illinois/NCSA Open Source License. See LICENSE.txt in the project root for license information.

using System;
using System.Linq;
using ClangSharp.Interop;

namespace ClangSharp
{
    public class CastExpr : Expr
    {
        private readonly Lazy<Expr> _subExpr;

        private protected CastExpr(CXCursor handle, CXCursorKind expectedCursorKind, CX_StmtClass expectedStmtClass) : base(handle, expectedCursorKind, expectedStmtClass)
        {
            if ((CX_StmtClass.CX_StmtClass_LastCastExpr < handle.StmtClass) || (handle.StmtClass < CX_StmtClass.CX_StmtClass_FirstCastExpr))
            {
                throw new ArgumentException(nameof(handle));
            }

            _subExpr = new Lazy<Expr>(() => Children.OfType<Expr>().Single());
        }

        public CX_CastKind CastKind => Handle.CastKind;

        public string CastKindSpelling => Handle.CastKindSpelling;

        public Expr SubExpr => _subExpr.Value;
    }
}
