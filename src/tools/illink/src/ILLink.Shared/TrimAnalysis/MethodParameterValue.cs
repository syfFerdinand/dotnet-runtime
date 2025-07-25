// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

// This is needed due to NativeAOT which doesn't enable nullable globally yet
#nullable enable


using System.Collections.Generic;
using ILLink.Shared.DataFlow;
using ILLink.Shared.TypeSystemProxy;

namespace ILLink.Shared.TrimAnalysis
{
    internal sealed partial record MethodParameterValue : ValueWithDynamicallyAccessedMembers, IValueWithStaticType
    {
        public TypeProxy? StaticType { get; }

        public ParameterProxy Parameter { get; }

        public override IEnumerable<string> GetDiagnosticArgumentsForAnnotationMismatch()
            => Parameter.GetDiagnosticArgumentsForAnnotationMismatch();

        public override string ToString()
            => this.ValueToString(Parameter.Method.Method, Parameter.Index, DynamicallyAccessedMemberTypes);

        public bool IsThisParameter() => Parameter.IsImplicitThis;

        public override SingleValue DeepCopy() => this; // This value is immutable

        public ParameterIndex Index => Parameter.Index;
    }
}
