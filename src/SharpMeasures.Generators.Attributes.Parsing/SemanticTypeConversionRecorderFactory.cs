namespace SharpMeasures.Generators.Attributes.Parsing;

using Microsoft.CodeAnalysis;

using OneOf;
using OneOf.Types;

using SharpAttributeParser;
using SharpAttributeParser.Mappers;

using System;
using System.Collections.Generic;

/// <inheritdoc cref="ISemanticTypeConversionRecorderFactory"/>
public sealed class SemanticTypeConversionRecorderFactory : ISemanticTypeConversionRecorderFactory
{
    private ISemanticRecorderFactory Factory { get; }
    private ISemanticMapper<ISemanticTypeConversionRecordBuilder> Mapper { get; }

    /// <summary>Instantiates a <see cref="SemanticTypeConversionRecorderFactory"/>, handling creation of <see cref="ISemanticRecorder{TRecord}"/> for recording the arguments of <see cref="TypeConversionAttribute"/>.</summary>
    /// <param name="factory">The factory used to create <see cref="ISemanticRecorder{TRecord}"/>.</param>
    /// <param name="mapper">Provides mappings from the parameters of <see cref="TypeConversionAttribute"/> to recorders.</param>
    public SemanticTypeConversionRecorderFactory(ISemanticRecorderFactory factory, ISemanticMapper<ISemanticTypeConversionRecordBuilder> mapper)
    {
        Factory = factory ?? throw new ArgumentNullException(nameof(factory));
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    ISemanticRecorder<ISemanticTypeConversionRecord> ISemanticRecorderFactory<ISemanticTypeConversionRecord>.Create()
    {
        return Factory.Create<ISemanticTypeConversionRecord, ISemanticTypeConversionRecordBuilder>(Mapper, new TypeConversionRecordBuilder());
    }

    private sealed class TypeConversionRecordBuilder : ARecordBuilder<ISemanticTypeConversionRecord>, ISemanticTypeConversionRecordBuilder
    {
        private TypeConversionRecord Target { get; } = new();
        private BuildTracker Tracker { get; set; } = new();

        public TypeConversionRecordBuilder() : base(throwOnMultipleBuilds: true) { }

        protected override ISemanticTypeConversionRecord GetRecord() => Target;
        protected override bool CanBuildRecord() => Tracker.Types;

        void ISemanticTypeConversionRecordBuilder.WithTypes(IReadOnlyList<ITypeSymbol?>? types)
        {
            VerifyCanModify();

            Target.Types = types;
            Tracker = Tracker.WithTypes();
        }

        void ISemanticTypeConversionRecordBuilder.WithForwardsImplementation(ConversionImplementation forwardsImplementation)
        {
            VerifyCanModify();

            Target.ForwardsImplementation = forwardsImplementation;
        }

        void ISemanticTypeConversionRecordBuilder.WithForwardsBehaviour(ConversionOperatorBehaviour forwardsBehaviour)
        {
            VerifyCanModify();

            Target.ForwardsBehaviour = forwardsBehaviour;
        }

        void ISemanticTypeConversionRecordBuilder.WithForwardsPropertyName(string? forwardsPropertyName)
        {
            VerifyCanModify();

            Target.ForwardsPropertyName = forwardsPropertyName;
        }

        void ISemanticTypeConversionRecordBuilder.WithForwardsMethodName(string? forwardsMethodName)
        {
            VerifyCanModify();

            Target.ForwardsMethodName = forwardsMethodName;
        }

        void ISemanticTypeConversionRecordBuilder.WithForwardsStaticMethodName(string? forwardsStaticMethodName)
        {
            VerifyCanModify();

            Target.ForwardsStaticMethodName = forwardsStaticMethodName;
        }

        void ISemanticTypeConversionRecordBuilder.WithBackwardsImplementation(ConversionImplementation backwardsImplementation)
        {
            VerifyCanModify();

            Target.BackwardsImplementation = backwardsImplementation;
        }

        void ISemanticTypeConversionRecordBuilder.WithBackwardsBehaviour(ConversionOperatorBehaviour backwardsBehaviour)
        {
            VerifyCanModify();

            Target.BackwardsBehaviour = backwardsBehaviour;
        }

        void ISemanticTypeConversionRecordBuilder.WithBackwardsStaticMethodName(string? backwardsStaticMethodName)
        {
            VerifyCanModify();

            Target.BackwardsStaticMethodName = backwardsStaticMethodName;
        }

        private readonly struct BuildTracker
        {
            public bool Types { get; private init; }

            public BuildTracker WithTypes() => this with { Types = true };
        }

        private sealed class TypeConversionRecord : ISemanticTypeConversionRecord
        {
            public IReadOnlyList<ITypeSymbol?>? Types { get; set; }

            public OneOf<None, ConversionImplementation> ForwardsImplementation { get; set; }
            public OneOf<None, ConversionOperatorBehaviour> ForwardsBehaviour { get; set; }
            public OneOf<None, string?> ForwardsPropertyName { get; set; }
            public OneOf<None, string?> ForwardsMethodName { get; set; }
            public OneOf<None, string?> ForwardsStaticMethodName { get; set; }

            public OneOf<None, ConversionImplementation> BackwardsImplementation { get; set; }
            public OneOf<None, ConversionOperatorBehaviour> BackwardsBehaviour { get; set; }
            public OneOf<None, string?> BackwardsStaticMethodName { get; set; }
        }
    }
}
