namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using OneOf;
using OneOf.Types;

using SharpAttributeParser;
using SharpAttributeParser.Mappers;

using SharpMeasures.Generators.Attributes.Quantities;

using System;
using System.Collections.Generic;

/// <inheritdoc cref="IQuantityConversionRecorderFactory"/>
public sealed class QuantityConversionRecorderFactory : IQuantityConversionRecorderFactory
{
    private ICombinedRecorderFactory Factory { get; }
    private ICombinedMapper<IQuantityConversionRecordBuilder> Mapper { get; }

    /// <summary>Instantiates a <see cref="QuantityConversionRecorderFactory"/>, handling creation of <see cref="ICombinedRecorder{TRecord}"/> for recording the arguments of <see cref="QuantityConversionAttribute"/>.</summary>
    /// <param name="factory">The factory used to create <see cref="ICombinedRecorder{TRecord}"/>.</param>
    /// <param name="mapper">Provides mappings from the parameters of <see cref="QuantityConversionAttribute"/> to recorders.</param>
    public QuantityConversionRecorderFactory(ICombinedRecorderFactory factory, ICombinedMapper<IQuantityConversionRecordBuilder> mapper)
    {
        Factory = factory ?? throw new ArgumentNullException(nameof(factory));
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    ICombinedRecorder<IQuantityConversionRecord> IRecorderFactory<IQuantityConversionRecord>.Create(AttributeSyntax attributeSyntax)
    {
        if (attributeSyntax is null)
        {
            throw new ArgumentNullException(nameof(attributeSyntax));
        }

        var recordBuilder = new QuantityConversionRecordBuilder(attributeSyntax);

        return Factory.Create<IQuantityConversionRecord, IQuantityConversionRecordBuilder>(Mapper, recordBuilder);
    }

    private sealed class QuantityConversionRecordBuilder : ARecordBuilder<IQuantityConversionRecord>, IQuantityConversionRecordBuilder
    {
        private QuantityConversionRecord Target { get; }
        private BuildTracker Tracker { get; set; } = new();

        public QuantityConversionRecordBuilder(AttributeSyntax attributeSyntax) : base(throwOnMultipleBuilds: true)
        {
            SyntacticQuantityConversionRecord syntactic = new(attributeSyntax);

            Target = new(syntactic);
        }

        protected override IQuantityConversionRecord GetRecord() => Target;
        protected override bool CanBuildRecord() => Tracker.Quantities;

        void IQuantityConversionRecordBuilder.WithQuantities(IReadOnlyList<ITypeSymbol?>? quantities, OneOf<ExpressionSyntax, IReadOnlyList<ExpressionSyntax>> syntax)
        {
            VerifyOneOfSyntax.Verify(syntax);

            VerifyCanModify();

            Target.Quantities = quantities;
            Target.Syntactic.Quantities = syntax;
            Tracker = Tracker.WithQuantities();
        }

        void IQuantityConversionRecordBuilder.WithForwardsImplementation(ConversionImplementation forwardsImplementation, OneOf<None, ExpressionSyntax> syntax)
        {
            VerifyOneOfSyntax.Verify(syntax);

            VerifyCanModify();

            Target.ForwardsImplementation = forwardsImplementation;
            Target.Syntactic.ForwardsImplementation = syntax;
        }

        void IQuantityConversionRecordBuilder.WithForwardsBehaviour(ConversionOperatorBehaviour forwardsBehaviour, OneOf<None, ExpressionSyntax> syntax)
        {
            VerifyOneOfSyntax.Verify(syntax);

            VerifyCanModify();

            Target.ForwardsBehaviour = forwardsBehaviour;
            Target.Syntactic.ForwardsBehaviour = syntax;
        }

        void IQuantityConversionRecordBuilder.WithForwardsPropertyName(string? forwardsPropertyName, OneOf<None, ExpressionSyntax> syntax)
        {
            VerifyOneOfSyntax.Verify(syntax);

            VerifyCanModify();

            Target.ForwardsPropertyName = forwardsPropertyName;
            Target.Syntactic.ForwardsPropertyName = syntax;
        }

        void IQuantityConversionRecordBuilder.WithForwardsMethodName(string? forwardsMethodName, OneOf<None, ExpressionSyntax> syntax)
        {
            VerifyOneOfSyntax.Verify(syntax);

            VerifyCanModify();

            Target.ForwardsMethodName = forwardsMethodName;
            Target.Syntactic.ForwardsMethodName = syntax;
        }

        void IQuantityConversionRecordBuilder.WithForwardsStaticMethodName(string? forwardsStaticMethodName, OneOf<None, ExpressionSyntax> syntax)
        {
            VerifyOneOfSyntax.Verify(syntax);

            VerifyCanModify();

            Target.ForwardsStaticMethodName = forwardsStaticMethodName;
            Target.Syntactic.ForwardsStaticMethodName = syntax;
        }

        void IQuantityConversionRecordBuilder.WithBackwardsImplementation(ConversionImplementation backwardsImplementation, OneOf<None, ExpressionSyntax> syntax)
        {
            VerifyOneOfSyntax.Verify(syntax);

            VerifyCanModify();

            Target.BackwardsImplementation = backwardsImplementation;
            Target.Syntactic.BackwardsImplementation = syntax;
        }

        void IQuantityConversionRecordBuilder.WithBackwardsBehaviour(ConversionOperatorBehaviour backwardsBehaviour, OneOf<None, ExpressionSyntax> syntax)
        {
            VerifyOneOfSyntax.Verify(syntax);

            VerifyCanModify();

            Target.BackwardsBehaviour = backwardsBehaviour;
            Target.Syntactic.BackwardsBehaviour = syntax;
        }

        void IQuantityConversionRecordBuilder.WithBackwardsStaticMethodName(string? backwardsStaticMethodName, OneOf<None, ExpressionSyntax> syntax)
        {
            VerifyOneOfSyntax.Verify(syntax);

            VerifyCanModify();

            Target.BackwardsStaticMethodName = backwardsStaticMethodName;
            Target.Syntactic.BackwardsStaticMethodName = syntax;
        }

        private readonly struct BuildTracker
        {
            public bool Quantities { get; private init; }

            public BuildTracker WithQuantities() => this with { Quantities = true };
        }

        private sealed class QuantityConversionRecord : IQuantityConversionRecord
        {
            public SyntacticQuantityConversionRecord Syntactic { get; }

            public IReadOnlyList<ITypeSymbol?>? Quantities { get; set; }

            public OneOf<None, ConversionImplementation> ForwardsImplementation { get; set; }
            public OneOf<None, ConversionOperatorBehaviour> ForwardsBehaviour { get; set; }
            public OneOf<None, string?> ForwardsPropertyName { get; set; }
            public OneOf<None, string?> ForwardsMethodName { get; set; }
            public OneOf<None, string?> ForwardsStaticMethodName { get; set; }

            public OneOf<None, ConversionImplementation> BackwardsImplementation { get; set; }
            public OneOf<None, ConversionOperatorBehaviour> BackwardsBehaviour { get; set; }
            public OneOf<None, string?> BackwardsStaticMethodName { get; set; }

            public QuantityConversionRecord(SyntacticQuantityConversionRecord syntactic)
            {
                Syntactic = syntactic;
            }

            ISyntacticQuantityConversionRecord IQuantityConversionRecord.Syntactic => Syntactic;
        }

        private sealed class SyntacticQuantityConversionRecord : ASyntacticRecord, ISyntacticQuantityConversionRecord
        {
            public OneOf<ExpressionSyntax, IReadOnlyList<ExpressionSyntax>> Quantities { get; set; }

            public OneOf<None, ExpressionSyntax> ForwardsImplementation { get; set; }
            public OneOf<None, ExpressionSyntax> ForwardsBehaviour { get; set; }
            public OneOf<None, ExpressionSyntax> ForwardsPropertyName { get; set; }
            public OneOf<None, ExpressionSyntax> ForwardsMethodName { get; set; }
            public OneOf<None, ExpressionSyntax> ForwardsStaticMethodName { get; set; }

            public OneOf<None, ExpressionSyntax> BackwardsImplementation { get; set; }
            public OneOf<None, ExpressionSyntax> BackwardsBehaviour { get; set; }
            public OneOf<None, ExpressionSyntax> BackwardsStaticMethodName { get; set; }

            public SyntacticQuantityConversionRecord(AttributeSyntax attribute) : base(attribute) { }
        }
    }
}
