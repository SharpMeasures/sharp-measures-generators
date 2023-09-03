namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using Microsoft.CodeAnalysis;

using OneOf;
using OneOf.Types;

using SharpAttributeParser;
using SharpAttributeParser.Mappers;

using SharpMeasures.Generators.Attributes.Quantities;

using System;

/// <inheritdoc cref="ISemanticQuantityOperationRecorderFactory"/>
public sealed class SemanticQuantityOperationRecorderFactory : ISemanticQuantityOperationRecorderFactory
{
    private ISemanticRecorderFactory Factory { get; }
    private ISemanticMapper<ISemanticQuantityOperationRecordBuilder> Mapper { get; }

    /// <summary>Instantiates a <see cref="SemanticQuantityOperationRecorderFactory"/>, handling creation of <see cref="ISemanticRecorder{TRecord}"/> for recording the arguments of <see cref="QuantityOperationAttribute{TResult, TOther}"/>.</summary>
    /// <param name="factory">The factory used to create <see cref="ISemanticRecorder{TRecord}"/>.</param>
    /// <param name="mapper">Provides mappings from the parameters of <see cref="QuantityOperationAttribute{TResult, TOther}"/> to recorders.</param>
    public SemanticQuantityOperationRecorderFactory(ISemanticRecorderFactory factory, ISemanticMapper<ISemanticQuantityOperationRecordBuilder> mapper)
    {
        Factory = factory ?? throw new ArgumentNullException(nameof(factory));
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    ISemanticRecorder<ISemanticQuantityOperationRecord> ISemanticQuantityOperationRecorderFactory.Create()
    {
        var recordBuilder = new QuantityOperationRecordBuilder();

        return Factory.Create<ISemanticQuantityOperationRecord, ISemanticQuantityOperationRecordBuilder>(Mapper, recordBuilder);
    }

    private sealed class QuantityOperationRecordBuilder : ARecordBuilder<ISemanticQuantityOperationRecord>, ISemanticQuantityOperationRecordBuilder
    {
        private QuantityOperationRecord Target { get; } = new();
        private BuildTracker Tracker { get; set; } = new();

        public QuantityOperationRecordBuilder() : base(throwOnMultipleBuilds: true) { }

        protected override ISemanticQuantityOperationRecord GetRecord() => Target;
        protected override bool CanBuildRecord() => Tracker.Result && Tracker.Other && Tracker.OperatorType;

        void ISemanticQuantityOperationRecordBuilder.WithResult(ITypeSymbol result)
        {
            if (result is null)
            {
                throw new ArgumentNullException(nameof(result));
            }

            VerifyCanModify();

            Target.Result = result;
            Tracker = Tracker.WithResult();
        }

        void ISemanticQuantityOperationRecordBuilder.WithOther(ITypeSymbol other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            VerifyCanModify();

            Target.Other = other;
            Tracker = Tracker.WithOther();
        }

        void ISemanticQuantityOperationRecordBuilder.WithOperatorType(OperatorType operatorType)
        {
            VerifyCanModify();

            Target.OperatorType = operatorType;
            Tracker = Tracker.WithOperatorType();
        }

        void ISemanticQuantityOperationRecordBuilder.WithPosition(OperationPosition position)
        {
            VerifyCanModify();

            Target.Position = position;
        }

        void ISemanticQuantityOperationRecordBuilder.WithMirrorMode(OperationMirrorMode mirrorMode)
        {
            VerifyCanModify();

            Target.MirrorMode = mirrorMode;
        }

        void ISemanticQuantityOperationRecordBuilder.WithImplementation(OperationImplementation implementation)
        {
            VerifyCanModify();

            Target.Implementation = implementation;
        }

        void ISemanticQuantityOperationRecordBuilder.WithMirroredImplementation(OperationImplementation mirroredImplementation)
        {
            VerifyCanModify();

            Target.MirroredImplementation = mirroredImplementation;
        }

        void ISemanticQuantityOperationRecordBuilder.WithMethodName(string? methodName)
        {
            VerifyCanModify();

            Target.MethodName = methodName;
        }

        void ISemanticQuantityOperationRecordBuilder.WithStaticMethodName(string? staticMethodName)
        {
            VerifyCanModify();

            Target.StaticMethodName = staticMethodName;
        }

        void ISemanticQuantityOperationRecordBuilder.WithMirroredMethodName(string? mirroredMethodName)
        {
            VerifyCanModify();

            Target.MirroredMethodName = mirroredMethodName;
        }

        void ISemanticQuantityOperationRecordBuilder.WithMirroredStaticMethodName(string? mirroredStaticMethodName)
        {
            VerifyCanModify();

            Target.MirroredStaticMethodName = mirroredStaticMethodName;
        }

        private readonly struct BuildTracker
        {
            public bool Result { get; private init; }
            public bool Other { get; private init; }
            public bool OperatorType { get; private init; }

            public BuildTracker WithResult() => this with { Result = true };
            public BuildTracker WithOther() => this with { Other = true };
            public BuildTracker WithOperatorType() => this with { OperatorType = true };
        }

        private sealed class QuantityOperationRecord : ISemanticQuantityOperationRecord
        {
            public ITypeSymbol Result { get; set; } = null!;
            public ITypeSymbol Other { get; set; } = null!;

            public OperatorType OperatorType { get; set; }
            public OneOf<None, OperationPosition> Position { get; set; }
            public OneOf<None, OperationMirrorMode> MirrorMode { get; set; }
            public OneOf<None, OperationImplementation> Implementation { get; set; }
            public OneOf<None, OperationImplementation> MirroredImplementation { get; set; }

            public OneOf<None, string?> MethodName { get; set; }
            public OneOf<None, string?> StaticMethodName { get; set; }
            public OneOf<None, string?> MirroredMethodName { get; set; }
            public OneOf<None, string?> MirroredStaticMethodName { get; set; }
        }
    }
}
