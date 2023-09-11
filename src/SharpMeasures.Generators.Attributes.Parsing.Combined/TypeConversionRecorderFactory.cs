namespace SharpMeasures.Generators.Attributes.Parsing;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using OneOf;
using OneOf.Types;

using SharpAttributeParser;
using SharpAttributeParser.Mappers;

using System;
using System.Collections.Generic;

/// <inheritdoc cref="ITypeConversionRecorderFactory"/>
public sealed class TypeConversionRecorderFactory : ITypeConversionRecorderFactory
{
    private ICombinedRecorderFactory Factory { get; }
    private ICombinedMapper<ITypeConversionRecordBuilder> Mapper { get; }

    /// <summary>Instantiates a <see cref="TypeConversionRecorderFactory"/>, handling creation of <see cref="ICombinedRecorder{TRecord}"/> for recording the arguments of <see cref="TypeConversionAttribute"/>.</summary>
    /// <param name="factory">The factory used to create <see cref="ICombinedRecorder{TRecord}"/>.</param>
    /// <param name="mapper">Provides mappings from the parameters of <see cref="TypeConversionAttribute"/> to recorders.</param>
    public TypeConversionRecorderFactory(ICombinedRecorderFactory factory, ICombinedMapper<ITypeConversionRecordBuilder> mapper)
    {
        Factory = factory ?? throw new ArgumentNullException(nameof(factory));
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    ICombinedRecorder<ITypeConversionRecord> IRecorderFactory<ITypeConversionRecord>.Create(AttributeSyntax attributeSyntax)
    {
        if (attributeSyntax is null)
        {
            throw new ArgumentNullException(nameof(attributeSyntax));
        }

        var recordBuilder = new TypeConversionRecordBuilder(attributeSyntax);

        return Factory.Create<ITypeConversionRecord, ITypeConversionRecordBuilder>(Mapper, recordBuilder);
    }

    private sealed class TypeConversionRecordBuilder : ARecordBuilder<ITypeConversionRecord>, ITypeConversionRecordBuilder
    {
        private TypeConversionRecord Target { get; }
        private BuildTracker Tracker { get; set; } = new();

        public TypeConversionRecordBuilder(AttributeSyntax attributeSyntax) : base(throwOnMultipleBuilds: true)
        {
            SyntacticTypeConversionRecord syntactic = new(attributeSyntax);

            Target = new(syntactic);
        }

        protected override ITypeConversionRecord GetRecord() => Target;
        protected override bool CanBuildRecord() => Tracker.Types;

        void ITypeConversionRecordBuilder.WithTypes(IReadOnlyList<ITypeSymbol?>? types, OneOf<ExpressionSyntax, IReadOnlyList<ExpressionSyntax>> syntax)
        {
            VerifyOneOfSyntax.Verify(syntax);

            VerifyCanModify();

            Target.Types = types;
            Target.Syntactic.Types = syntax;
            Tracker = Tracker.WithTypes();
        }

        void ITypeConversionRecordBuilder.WithForwardsImplementation(ConversionImplementation forwardsImplementation, OneOf<None, ExpressionSyntax> syntax)
        {
            VerifyOneOfSyntax.Verify(syntax);

            VerifyCanModify();

            Target.ForwardsImplementation = forwardsImplementation;
            Target.Syntactic.ForwardsImplementation = syntax;
        }

        void ITypeConversionRecordBuilder.WithForwardsBehaviour(ConversionOperatorBehaviour forwardsBehaviour, OneOf<None, ExpressionSyntax> syntax)
        {
            VerifyOneOfSyntax.Verify(syntax);

            VerifyCanModify();

            Target.ForwardsBehaviour = forwardsBehaviour;
            Target.Syntactic.ForwardsBehaviour = syntax;
        }

        void ITypeConversionRecordBuilder.WithForwardsPropertyName(string? forwardsPropertyName, OneOf<None, ExpressionSyntax> syntax)
        {
            VerifyOneOfSyntax.Verify(syntax);

            VerifyCanModify();

            Target.ForwardsPropertyName = forwardsPropertyName;
            Target.Syntactic.ForwardsPropertyName = syntax;
        }

        void ITypeConversionRecordBuilder.WithForwardsMethodName(string? forwardsMethodName, OneOf<None, ExpressionSyntax> syntax)
        {
            VerifyOneOfSyntax.Verify(syntax);

            VerifyCanModify();

            Target.ForwardsMethodName = forwardsMethodName;
            Target.Syntactic.ForwardsMethodName = syntax;
        }

        void ITypeConversionRecordBuilder.WithForwardsStaticMethodName(string? forwardsStaticMethodName, OneOf<None, ExpressionSyntax> syntax)
        {
            VerifyOneOfSyntax.Verify(syntax);

            VerifyCanModify();

            Target.ForwardsStaticMethodName = forwardsStaticMethodName;
            Target.Syntactic.ForwardsStaticMethodName = syntax;
        }

        void ITypeConversionRecordBuilder.WithBackwardsImplementation(ConversionImplementation backwardsImplementation, OneOf<None, ExpressionSyntax> syntax)
        {
            VerifyOneOfSyntax.Verify(syntax);

            VerifyCanModify();

            Target.BackwardsImplementation = backwardsImplementation;
            Target.Syntactic.BackwardsImplementation = syntax;
        }

        void ITypeConversionRecordBuilder.WithBackwardsBehaviour(ConversionOperatorBehaviour backwardsBehaviour, OneOf<None, ExpressionSyntax> syntax)
        {
            VerifyOneOfSyntax.Verify(syntax);

            VerifyCanModify();

            Target.BackwardsBehaviour = backwardsBehaviour;
            Target.Syntactic.BackwardsBehaviour = syntax;
        }

        void ITypeConversionRecordBuilder.WithBackwardsStaticMethodName(string? backwardsStaticMethodName, OneOf<None, ExpressionSyntax> syntax)
        {
            VerifyOneOfSyntax.Verify(syntax);

            VerifyCanModify();

            Target.BackwardsStaticMethodName = backwardsStaticMethodName;
            Target.Syntactic.BackwardsStaticMethodName = syntax;
        }

        private readonly struct BuildTracker
        {
            public bool Types { get; private init; }

            public BuildTracker WithTypes() => this with { Types = true };
        }

        private sealed class TypeConversionRecord : ITypeConversionRecord
        {
            public SyntacticTypeConversionRecord Syntactic { get; }

            public IReadOnlyList<ITypeSymbol?>? Types { get; set; }

            public OneOf<None, ConversionImplementation> ForwardsImplementation { get; set; }
            public OneOf<None, ConversionOperatorBehaviour> ForwardsBehaviour { get; set; }
            public OneOf<None, string?> ForwardsPropertyName { get; set; }
            public OneOf<None, string?> ForwardsMethodName { get; set; }
            public OneOf<None, string?> ForwardsStaticMethodName { get; set; }

            public OneOf<None, ConversionImplementation> BackwardsImplementation { get; set; }
            public OneOf<None, ConversionOperatorBehaviour> BackwardsBehaviour { get; set; }
            public OneOf<None, string?> BackwardsStaticMethodName { get; set; }

            public TypeConversionRecord(SyntacticTypeConversionRecord syntactic)
            {
                Syntactic = syntactic;
            }

            ISyntacticTypeConversionRecord ITypeConversionRecord.Syntactic => Syntactic;
        }

        private sealed class SyntacticTypeConversionRecord : ASyntacticRecord, ISyntacticTypeConversionRecord
        {
            public OneOf<ExpressionSyntax, IReadOnlyList<ExpressionSyntax>> Types { get; set; }

            public OneOf<None, ExpressionSyntax> ForwardsImplementation { get; set; }
            public OneOf<None, ExpressionSyntax> ForwardsBehaviour { get; set; }
            public OneOf<None, ExpressionSyntax> ForwardsPropertyName { get; set; }
            public OneOf<None, ExpressionSyntax> ForwardsMethodName { get; set; }
            public OneOf<None, ExpressionSyntax> ForwardsStaticMethodName { get; set; }

            public OneOf<None, ExpressionSyntax> BackwardsImplementation { get; set; }
            public OneOf<None, ExpressionSyntax> BackwardsBehaviour { get; set; }
            public OneOf<None, ExpressionSyntax> BackwardsStaticMethodName { get; set; }

            public SyntacticTypeConversionRecord(AttributeSyntax attribute) : base(attribute) { }
        }
    }
}
