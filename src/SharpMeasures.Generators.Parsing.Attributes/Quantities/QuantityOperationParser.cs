namespace SharpMeasures.Generators.Parsing.Attributes.Quantities;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using System;
using System.Collections.Generic;

/// <summary>Allows the arguments of a <see cref="QuantityOperationAttribute{TResult, TOther}"/> to be parsed.</summary>
public sealed class QuantityOperationParser : ISyntacticQuantityOperationParser, ISemanticQuantityOperationParser
{
    private ISyntacticAttributeParser SyntacticParser { get; }
    private ISemanticAttributeParser SemanticParser { get; }

    /// <summary>Instantiates a <see cref="QuantityOperationParser"/>, parsing the arguments of a <see cref="QuantityOperationAttribute{TResult, TOther}"/>.</summary>
    /// <param name="syntacticParser"><inheritdoc cref="ISyntacticAttributeParser" path="/summary"/></param>
    /// <param name="semanticParser"><inheritdoc cref="ISemanticAttributeParser" path="/summary"/></param>
    /// <exception cref="ArgumentNullException"/>
    public QuantityOperationParser(ISyntacticAttributeParser syntacticParser, ISemanticAttributeParser semanticParser)
    {
        SyntacticParser = syntacticParser ?? throw new ArgumentNullException(nameof(syntacticParser));
        SemanticParser = semanticParser ?? throw new ArgumentNullException(nameof(semanticParser));
    }

    /// <inheritdoc/>
    public ISyntacticQuantityOperation? TryParse(AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        if (attributeSyntax is null)
        {
            throw new ArgumentNullException(nameof(attributeSyntax));
        }

        QuantityOperationAttributeArgumentRecorder recorder = new();

        if (SyntacticParser.TryParse(recorder, attributeData, attributeSyntax) is false)
        {
            return null;
        }

        recorder.RecordAttributeLocations(attributeSyntax);

        return CreateSyntactic(recorder);
    }

    /// <inheritdoc/>
    public IQuantityOperation? TryParse(AttributeData attributeData)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        QuantityOperationAttributeArgumentRecorder recorder = new();

        if (SemanticParser.TryParse(recorder, attributeData) is false)
        {
            return null;
        }

        return CreateSemantic(recorder);
    }

    private static ISyntacticQuantityOperation? CreateSyntactic(QuantityOperationAttributeArgumentRecorder recorder)
    {
        if (CreateSemantic(recorder) is not IQuantityOperation semantics)
        {
            return null;
        }

        return new SyntacticQuantityOperation(semantics, CreateSyntax(recorder));
    }

    private static IQuantityOperation? CreateSemantic(QuantityOperationAttributeArgumentRecorder recorder)
    {
        if (recorder.Result is null || recorder.Other is null || recorder.OperatorType is null)
        {
            return null;
        }

        return new SemanticQuantityOperation(recorder.Result, recorder.Other, recorder.OperatorType.Value, recorder.Position, recorder.MirrorMode, recorder.Implementation, recorder.MirroredImplementation, recorder.MethodName,
            recorder.StaticMethodName, recorder.MirroredMethodName, recorder.MirroredStaticMethodName);
    }

    private static IQuantityOperationSyntax CreateSyntax(QuantityOperationAttributeArgumentRecorder recorder)
    {
        return new QuantityOperationSyntax(recorder.AttributeNameLocation, recorder.AttributeLocation, recorder.ResultLocation, recorder.OtherLocation, recorder.OperatorTypeLocation, recorder.PositionLocation, recorder.MirrorModeLocation, recorder.ImplementationLocation,
            recorder.MirroredImplementationLocation, recorder.MethodNameLocation, recorder.StaticMethodNameLocation, recorder.MirroredMethodNameLocation, recorder.MirroredStaticMethodNameLocation);
    }

    private sealed class QuantityOperationAttributeArgumentRecorder : Attributes.AArgumentRecorder
    {
        public ITypeSymbol? Result { get; private set; }
        public ITypeSymbol? Other { get; private set; }

        public OperatorType? OperatorType { get; private set; }
        public OperationPosition? Position { get; private set; }
        public OperationMirrorMode? MirrorMode { get; private set; }
        public OperationImplementation? Implementation { get; private set; }
        public OperationImplementation? MirroredImplementation { get; private set; }

        public string? MethodName { get; private set; }
        public string? StaticMethodName { get; private set; }
        public string? MirroredMethodName { get; private set; }
        public string? MirroredStaticMethodName { get; private set; }

        public Location ResultLocation { get; private set; } = Location.None;
        public Location OtherLocation { get; private set; } = Location.None;

        public Location OperatorTypeLocation { get; private set; } = Location.None;
        public Location PositionLocation { get; private set; } = Location.None;
        public Location MirrorModeLocation { get; private set; } = Location.None;
        public Location ImplementationLocation { get; private set; } = Location.None;
        public Location MirroredImplementationLocation { get; private set; } = Location.None;

        public Location MethodNameLocation { get; private set; } = Location.None;
        public Location StaticMethodNameLocation { get; private set; } = Location.None;
        public Location MirroredMethodNameLocation { get; private set; } = Location.None;
        public Location MirroredStaticMethodNameLocation { get; private set; } = Location.None;

        protected override IEnumerable<(string, DSyntacticGenericRecorder)> AddGenericRecorders()
        {
            yield return ("TResult", Adapters.For(RecordResult));
            yield return ("TOther", Adapters.For(RecordOther));
        }

        protected override IEnumerable<(string, DSyntacticSingleRecorder)> AddSingleRecorders()
        {
            yield return ("OperatorType", Adapters.For<OperatorType>(RecordOperatorType));
            yield return ("Position", Adapters.For<OperationPosition>(RecordPosition));
            yield return ("MirrorMode", Adapters.For<OperationMirrorMode>(RecordMirrorMode));
            yield return ("Implementation", Adapters.For<OperationImplementation>(RecordImplementation));
            yield return ("MirroredImplementation", Adapters.For<OperationImplementation>(RecordMirroredImplementation));
            yield return ("MethodName", Adapters.ForNullable<string>(RecordMethodName));
            yield return ("StaticMethodName", Adapters.ForNullable<string>(RecordStaticMethodName));
            yield return ("MirroredMethodName", Adapters.ForNullable<string>(RecordMirroredMethodName));
            yield return ("MirroredStaticMethodName", Adapters.ForNullable<string>(RecordMirroredStaticMethodName));
        }

        private void RecordResult(ITypeSymbol result, Location location)
        {
            Result = result;
            ResultLocation = location;
        }

        private void RecordOther(ITypeSymbol other, Location location)
        {
            Other = other;
            OtherLocation = location;
        }

        private void RecordOperatorType(OperatorType operatorType, Location location)
        {
            OperatorType = operatorType;
            OperatorTypeLocation = location;
        }

        private void RecordPosition(OperationPosition position, Location location)
        {
            Position = position;
            PositionLocation = location;
        }

        private void RecordMirrorMode(OperationMirrorMode mirrorMode, Location location)
        {
            MirrorMode = mirrorMode;
            MirrorModeLocation = location;
        }

        private void RecordImplementation(OperationImplementation implementation, Location location)
        {
            Implementation = implementation;
            ImplementationLocation = location;
        }

        private void RecordMirroredImplementation(OperationImplementation mirroredImplementation, Location location)
        {
            MirroredImplementation = mirroredImplementation;
            MirroredImplementationLocation = location;
        }

        private void RecordMethodName(string? methodName, Location location)
        {
            if (methodName is not null)
            {
                MethodName = methodName;
            }

            MethodNameLocation = location;
        }

        private void RecordStaticMethodName(string? staticMethodName, Location location)
        {
            if (staticMethodName is not null)
            {
                StaticMethodName = staticMethodName;
            }

            StaticMethodNameLocation = location;
        }

        private void RecordMirroredMethodName(string? mirroredMethodName, Location location)
        {
            if (mirroredMethodName is not null)
            {
                MirroredMethodName = mirroredMethodName;
            }

            MirroredMethodNameLocation = location;
        }

        private void RecordMirroredStaticMethodName(string? mirroredStaticMethodName, Location location)
        {
            if (mirroredStaticMethodName is not null)
            {
                MirroredStaticMethodName = mirroredStaticMethodName;
            }

            MirroredStaticMethodNameLocation = location;
        }
    }

    private sealed class SyntacticQuantityOperation : ISyntacticQuantityOperation
    {
        private IQuantityOperation Semantics { get; }
        private IQuantityOperationSyntax Syntax { get; }

        public SyntacticQuantityOperation(IQuantityOperation semantics, IQuantityOperationSyntax syntax)
        {
            Semantics = semantics;
            Syntax = syntax;
        }

        ITypeSymbol IQuantityOperation.Result => Semantics.Result;
        ITypeSymbol IQuantityOperation.Other => Semantics.Other;

        OperatorType IQuantityOperation.OperatorType => Semantics.OperatorType;
        OperationPosition? IQuantityOperation.Position => Semantics.Position;
        OperationMirrorMode? IQuantityOperation.MirrorMode => Semantics.MirrorMode;
        OperationImplementation? IQuantityOperation.Implementation => Semantics.Implementation;
        OperationImplementation? IQuantityOperation.MirroredImplementation => Semantics.MirroredImplementation;

        string? IQuantityOperation.MethodName => Semantics.MethodName;
        string? IQuantityOperation.StaticMethodName => Semantics.StaticMethodName;
        string? IQuantityOperation.MirroredMethodName => Semantics.MirroredMethodName;
        string? IQuantityOperation.MirroredStaticMethodName => Semantics.MirroredStaticMethodName;

        IQuantityOperationSyntax ISyntacticQuantityOperation.Syntax => Syntax;
    }

    private sealed class SemanticQuantityOperation : IQuantityOperation
    {
        private ITypeSymbol Result { get; }
        private ITypeSymbol Other { get; }

        private OperatorType OperatorType { get; }
        private OperationPosition? Position { get; }
        private OperationMirrorMode? MirrorMode { get; }
        private OperationImplementation? Implementation { get; }
        private OperationImplementation? MirroredImplementation { get; }

        private string? MethodName { get; }
        private string? StaticMethodName { get; }
        private string? MirroredMethodName { get; }
        private string? MirroredStaticMethodName { get; }

        public SemanticQuantityOperation(ITypeSymbol result, ITypeSymbol other, OperatorType operatorType, OperationPosition? position, OperationMirrorMode? mirrorMode, OperationImplementation? implementation,
            OperationImplementation? mirroredImplementation, string? methodName, string? staticMethodName, string? mirroredMethodName, string? mirroredStaticMethodName)
        {
            Result = result;
            Other = other;

            OperatorType = operatorType;
            Position = position;
            MirrorMode = mirrorMode;
            Implementation = implementation;
            MirroredImplementation = mirroredImplementation;

            MethodName = methodName;
            StaticMethodName = staticMethodName;
            MirroredMethodName = mirroredMethodName;
            MirroredStaticMethodName = mirroredStaticMethodName;
        }

        ITypeSymbol IQuantityOperation.Result => Result;
        ITypeSymbol IQuantityOperation.Other => Other;

        OperatorType IQuantityOperation.OperatorType => OperatorType;
        OperationPosition? IQuantityOperation.Position => Position;
        OperationMirrorMode? IQuantityOperation.MirrorMode => MirrorMode;
        OperationImplementation? IQuantityOperation.Implementation => Implementation;
        OperationImplementation? IQuantityOperation.MirroredImplementation => MirroredImplementation;

        string? IQuantityOperation.MethodName => MethodName;
        string? IQuantityOperation.StaticMethodName => StaticMethodName;
        string? IQuantityOperation.MirroredMethodName => MirroredMethodName;
        string? IQuantityOperation.MirroredStaticMethodName => MirroredStaticMethodName;
    }

    private sealed class QuantityOperationSyntax : AAttributeSyntax, IQuantityOperationSyntax
    {
        private Location Result { get; }
        private Location Other { get; }

        private Location OperatorType { get; }
        private Location Position { get; }
        private Location MirrorMode { get; }
        private Location Implementation { get; }
        private Location MirroredImplementation { get; }

        private Location MethodName { get; }
        private Location StaticMethodName { get; }
        private Location MirroredMethodName { get; }
        private Location MirroredStaticMethodName { get; }

        public QuantityOperationSyntax(Location attributeName, Location attribute, Location result, Location other, Location operatorType, Location position, Location mirrorMode, Location implementation, Location mirroredImplementation, Location methodName,
            Location staticMethodName, Location mirroredMethodName, Location mirroredStaticMethodName) : base(attributeName, attribute)
        {
            Result = result;
            Other = other;

            OperatorType = operatorType;
            Position = position;
            MirrorMode = mirrorMode;
            Implementation = implementation;
            MirroredImplementation = mirroredImplementation;

            MethodName = methodName;
            StaticMethodName = staticMethodName;
            MirroredMethodName = mirroredMethodName;
            MirroredStaticMethodName = mirroredStaticMethodName;
        }

        Location IQuantityOperationSyntax.Result => Result;
        Location IQuantityOperationSyntax.Other => Other;

        Location IQuantityOperationSyntax.OperatorType => OperatorType;
        Location IQuantityOperationSyntax.Position => Position;
        Location IQuantityOperationSyntax.MirrorMode => MirrorMode;
        Location IQuantityOperationSyntax.Implementation => Implementation;
        Location IQuantityOperationSyntax.MirroredImplementation => MirroredImplementation;

        Location IQuantityOperationSyntax.MethodName => MethodName;
        Location IQuantityOperationSyntax.StaticMethodName => StaticMethodName;
        Location IQuantityOperationSyntax.MirroredMethodName => MirroredMethodName;
        Location IQuantityOperationSyntax.MirroredStaticMethodName => MirroredStaticMethodName;
    }
}
