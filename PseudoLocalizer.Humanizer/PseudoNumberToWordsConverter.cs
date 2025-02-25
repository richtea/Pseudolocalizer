﻿using System;
using Humanizer;
using Humanizer.Configuration;
using Humanizer.Localisation.NumberToWords;
using PseudoLocalizer.Core;

namespace PseudoLocalizer.Humanizer
{
    /// <summary>
    /// A class representing an implementation of <see cref="INumberToWordsConverter"/> that applies
    /// pseudo-localization using a specified <see cref="ITransformer"/>. This class cannot be inherited.
    /// </summary>
    public sealed class PseudoNumberToWordsConverter : INumberToWordsConverter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PseudoNumberToWordsConverter"/> class.
        /// </summary>
        /// <param name="pseudoHumanizer">The <see cref="PseudoHumanizer"/> to use.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="pseudoHumanizer"/> is <see langword="null"/>.
        /// </exception>
        public PseudoNumberToWordsConverter(PseudoHumanizer pseudoHumanizer)
        {
            Transformer = pseudoHumanizer ?? throw new ArgumentNullException(nameof(pseudoHumanizer));
            Inner = Configurator.NumberToWordsConverters.ResolveForCulture(pseudoHumanizer.BaseLocale);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PseudoNumberToWordsConverter"/> class.
        /// </summary>
        /// <param name="transformer">The <see cref="ITransformer"/> to use.</param>
        /// <param name="inner">The inner <see cref="INumberToWordsConverter"/> to use before applying transforms.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="transformer"/> or <paramref name="inner"/> is <see langword="null"/>.
        /// </exception>
        public PseudoNumberToWordsConverter(ITransformer transformer, INumberToWordsConverter inner)
        {
            Transformer = transformer ?? throw new ArgumentNullException(nameof(transformer));
            Inner = inner ?? throw new ArgumentNullException(nameof(inner));
        }

        /// <summary>
        /// Gets the inner <see cref="INumberToWordsConverter"/> to use.
        /// </summary>
        private INumberToWordsConverter Inner { get; }

        /// <summary>
        /// Gets the <see cref="ITransformer"/> to use.
        /// </summary>
        private ITransformer Transformer { get; }

        /// <inheritdoc />
        public string Convert(long number)
                => Transformer.Transform(Inner.Convert(number));

        /// <inheritdoc />
        public string Convert(long number, GrammaticalGender gender)
            => Transformer.Transform(Inner.Convert(number, gender));

        /// <inheritdoc />
        public string ConvertToOrdinal(int number)
            => Transformer.Transform(Inner.ConvertToOrdinal(number));

        /// <inheritdoc />
        public string ConvertToOrdinal(int number, GrammaticalGender gender)
            => Transformer.Transform(Inner.ConvertToOrdinal(number, gender));
    }
}
