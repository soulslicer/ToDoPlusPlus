//@ivan A0086401M
using System.Collections.Generic;

namespace ToDo
{
    class CommandParser
    {
        StringParser stringParser;
        TokenGenerator tokenFactory;
        OperationGenerator operationFactory;

        /// <summary>
        /// Constructor for the CommandParser class.
        /// </summary>
        public CommandParser()
        {
            this.stringParser = new StringParser();
            this.tokenFactory = new TokenGenerator();
            this.operationFactory = new OperationGenerator();
        }

        /// <summary>
        /// Parses a input string and returns the Operation that can be executed.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>An operation representing the input command.</returns>
        public Operation ParseOperation(string input)
        {
            List<string> words = stringParser.ParseStringIntoWords(input);
            List<Token> tokens = tokenFactory.GenerateAllTokens(words);
            return GenerateOperation(tokens);       
        }

        /// <summary>
        /// This method uses the given list of tokens to generate a corresponding Operation.
        /// </summary>
        /// <param name="tokens">The list of tokens from which the generated operation will be based on.</param>
        /// <returns>The generated Operation.</returns>
        private Operation GenerateOperation(List<Token> tokens)
        {            
            // reset factory configuration
            operationFactory.InitializeNewConfiguration();
            foreach (Token token in tokens)
            {
                token.ConfigureGenerator(operationFactory);
            }
            // implement? ReleaseUnusedTokens();
            operationFactory.FinalizeGenerator();
            Operation newOperation = operationFactory.CreateOperation();
            return newOperation;
        }

    }
}
