// MIT License
//
// Copyright (c) Microsoft Corporation. All rights reserved.
//
//  Permission is hereby granted, free of charge, to any person obtaining a copy
//  of this software and associated documentation files (the "Software"), to deal
//  in the Software without restriction, including without limitation the rights
//  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//  copies of the Software, and to permit persons to whom the Software is
//  furnished to do so, subject to the following conditions:
//
//  The above copyright notice and this permission notice shall be included in all
//  copies or substantial portions of the Software.
//
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//  SOFTWARE

// Copyright (c) Microsoft. All rights reserved.
// This code is licensed under the MIT License (MIT).
// THE CODE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
// DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
// TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH
// THE CODE OR THE USE OR OTHER DEALINGS IN THE CODE.

// ******************************************************************

using Microsoft.Knowzy.Common.Contracts;
using Microsoft.Knowzy.Common.Contracts.Helpers;
using Microsoft.Knowzy.Domain;

namespace Microsoft.Knowzy.DataProvider
{
    public class JsonDataProvider : IDataProvider
    {
        private readonly IConfigurationService _configuration;
        private readonly IFileHelper _fileHelper;
        private readonly IJsonHelper _jsonHelper;

        public JsonDataProvider(IConfigurationService configuration, IFileHelper fileHelper, IJsonHelper jsonHelper)
        {
            _configuration = configuration;
            _fileHelper = fileHelper;
            _jsonHelper = jsonHelper;
        }

        public Product[] GetData()
        {
            var jsonFilePath = _configuration.Configuration.JsonFilePath;

            return _jsonHelper.Deserialize<Product[]>(_fileHelper.ReadTextFile(_fileHelper.ActualPath + jsonFilePath));
        }

        public void SetData(Product[] products)
        {
            var jsonFilePath = _configuration.Configuration.JsonFilePath;

            try
            {
                _fileHelper.WriteTextFile(_fileHelper.ActualPath + jsonFilePath, _jsonHelper.Serialize(products));
            }
            catch
            { }
        }
    }
}
