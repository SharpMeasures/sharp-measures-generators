namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using Microsoft.CodeAnalysis;

using OneOf;
using OneOf.Types;

using SharpAttributeParser;
using SharpAttributeParser.Mappers;

using SharpMeasures.Generators.Attributes.Quantities;

using System;
using System.Collections.Generic;

/// <inheritdoc cref="ISemanticQuantityConversionRecorderFactory"/>
public sealed class SemanticQuantityConversionRecorderFactory : ISemanticQuantityConversionRecorderFactory
{
    private ISemanticRecorderFactory Factory { get; }
    private ISemanticMapper<ISemanticQuantityConversionRecordBuilder> Mapper { get; }

    /// <summary>Instantiates a <see cref="SemanticQuantityConversionRecorderFactory"/>, handling creation of <see cref="ISemanticRecorder{TRecord}"/> for recording the arguments of <see cref="QuantityConversionAttribute"/>.</summary>
    /// <param name="factory">The factory used to create <see cref="ISemanticRecorder{TRecord}"/>.</param>
    /// <param name="mapper">Provides mappings from the parameters of <see cref="QuantityConversionAttribute"/> to recorders.</param>
    public SemanticQuantityConversionRecorderFactory(ISemanticRecorderFactory factory, ISemanticMapper<ISemanticQuantityConversionRecordBuilder> mapper)
    {
        Factory = factory ?? throw new ArgumentNullException(nameof(factory));
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    ISemanticRecorder<ISemanticQuantityConversionRecord> ISemanticQuantityConversionRecorderFactory.Create()
    {
        return Factory.Create<ISemanticQuantityConversionRecord, ISemanticQuantityConversionRecordBuilder>(Mapper, new QuantityConversionRecordBuilder());
    }

    private sealed class QuantityConversionRecordBuilder : ARecordBuilder<ISemanticQuantityConversionRecord>, ISemanticQuantityConversionRecordBuilder
    {
        private QuantityConversionRecord Target { get; } = new();
        private BuildTracker Tracker { get; set; } = new();

        public QuantityConversionRecordBuilder() : base(throwOnMultipleBuilds: true) { }

        protected override ISemanticQuantityConversionRecord GetRecord() => Target;
        protected override bool CanBuildRecord() => Tracker.Quantities;

        void ISemanticQuantityConversionRecordBuilder.WithQuantities(IReadOnlyList<ITypeSymbol?>? quantities)
        {
            VerifyCanModify();

            Target.Quantities = quantities;
            Tracker = Tracker.WithQuantities();
        }

        void ISemanticQuantityConversionRecordBuilder.WithForwardsImplementation(ConversionImplementation forwardsImplementation)
        {
            VerifyCanModify();

            Target.ForwardsImplementation = forwardsImplementation;
        }

        void ISemanticQuantityConversionRecordBuilder.WithForwardsBehaviour(ConversionOperatorBehaviour forwardsBehaviour)
        {
            VerifyCanModify();

            Target.ForwardsBehaviour = forwardsBehaviour;
        }

        void ISemanticQuantityConversionRecordBuilder.WithForwardsPropertyName(string? forwardsPropertyName)
        {
            VerifyCanModify();

            Target.ForwardsPropertyName = forwardsPropertyName;
        }

        void ISemanticQuantityConversionRecordBuilder.WithForwardsMethodName(string? forwardsMethodName)
        {
            VerifyCanModify();

            Target.ForwardsMethodName = forwardsMethodName;
        }

        void ISemanticQuantityConversionRecordBuilder.WithForwardsStaticMethodName(string? forwardsStaticMethodName)
        {
            VerifyCanModify();

            Target.ForwardsStaticMethodName = forwardsStaticMethodName;
        }

        void ISemanticQuantityConversionRecordBuilder.WithBackwardsImplementation(ConversionImplementation backwardsImplementation)
        {
            VerifyCanModify();

            Target.BackwardsImplementation = backwardsImplementation;
        }

        void ISemanticQuantityConversionRecordBuilder.WithBackwardsBehaviour(ConversionOperatorBehaviour backwardsBehaviour)
        {
            VerifyCanModify();

            Target.BackwardsBehaviour = backwardsBehaviour;
        }

        void ISemanticQuantityConversionRecordBuilder.WithBackwardsStaticMethodName(string? backwardsStaticMethodName)
        {
            VerifyCanModify();

            Target.BackwardsStaticMethodName = backwardsStaticMethodName;
        }

        private readonly struct BuildTracker
        {
            public bool Quantities { get; private init; }

            public BuildTracker WithQuantities() => this with { Quantities = true };
        }

        private sealed class QuantityConversionRecord : ISemanticQuantityConversionRecord
        {
            public IReadOnlyList<ITypeSymbol?>? Quantities { get; set; }

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
