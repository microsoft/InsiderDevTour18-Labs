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

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Media;
using Windows.Storage;
using Windows.AI.MachineLearning.Preview;

// CNTKGraph

namespace AlarmClock
{
    public sealed class CNTKGraphModelInput
    {
        public VideoFrame Input338 { get; set; }
    }

    public sealed class CNTKGraphModelOutput
    {
        public IList<float> Plus692_Output_0 { get; set; }
        public CNTKGraphModelOutput()
        {
            this.Plus692_Output_0 = new List<float>();
        }
    }

    public sealed class CNTKGraphModel
    {
        private LearningModelPreview learningModel;
        public static async Task<CNTKGraphModel> CreateCNTKGraphModel(StorageFile file)
        {
            LearningModelPreview learningModel = await LearningModelPreview.LoadModelFromStorageFileAsync(file);
            CNTKGraphModel model = new CNTKGraphModel();
            model.learningModel = learningModel;
            return model;
        }
        public async Task<CNTKGraphModelOutput> EvaluateAsync(CNTKGraphModelInput input) {
            CNTKGraphModelOutput output = new CNTKGraphModelOutput();
            LearningModelBindingPreview binding = new LearningModelBindingPreview(learningModel);
            binding.Bind("Input338", input.Input338);
            binding.Bind("Plus692_Output_0", output.Plus692_Output_0);
            LearningModelEvaluationResultPreview evalResult = await learningModel.EvaluateAsync(binding, string.Empty);
            return output;
        }
    }
}
