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

// af4dec16-0900-4f35-8d03-28743433843e_cb36632a-770d-4ef8-a996-d3b067ff0e11

namespace ContosoIT
{
    public sealed class Af4dec16_x002D_0900_x002D_4f35_x002D_8d03_x002D_28743433843e_cb36632a_x002D_770d_x002D_4ef8_x002D_a996_x002D_d3b067ff0e11ModelInput
    {
        public VideoFrame data { get; set; }
    }

    public sealed class Af4dec16_x002D_0900_x002D_4f35_x002D_8d03_x002D_28743433843e_cb36632a_x002D_770d_x002D_4ef8_x002D_a996_x002D_d3b067ff0e11ModelOutput
    {
        public IList<string> classLabel { get; set; }
        public IDictionary<string, float> loss { get; set; }
        public Af4dec16_x002D_0900_x002D_4f35_x002D_8d03_x002D_28743433843e_cb36632a_x002D_770d_x002D_4ef8_x002D_a996_x002D_d3b067ff0e11ModelOutput()
        {
            this.classLabel = new List<string>();
            this.loss = new Dictionary<string, float>()
            {
                { "Surface Pro", float.NaN },
                { "Surface Studio", float.NaN },
            };
        }
    }

    public sealed class Af4dec16_x002D_0900_x002D_4f35_x002D_8d03_x002D_28743433843e_cb36632a_x002D_770d_x002D_4ef8_x002D_a996_x002D_d3b067ff0e11Model
    {
        private LearningModelPreview learningModel;
        public static async Task<Af4dec16_x002D_0900_x002D_4f35_x002D_8d03_x002D_28743433843e_cb36632a_x002D_770d_x002D_4ef8_x002D_a996_x002D_d3b067ff0e11Model> CreateAf4dec16_x002D_0900_x002D_4f35_x002D_8d03_x002D_28743433843e_cb36632a_x002D_770d_x002D_4ef8_x002D_a996_x002D_d3b067ff0e11Model(StorageFile file)
        {
            LearningModelPreview learningModel = await LearningModelPreview.LoadModelFromStorageFileAsync(file);
            Af4dec16_x002D_0900_x002D_4f35_x002D_8d03_x002D_28743433843e_cb36632a_x002D_770d_x002D_4ef8_x002D_a996_x002D_d3b067ff0e11Model model = new Af4dec16_x002D_0900_x002D_4f35_x002D_8d03_x002D_28743433843e_cb36632a_x002D_770d_x002D_4ef8_x002D_a996_x002D_d3b067ff0e11Model();
            model.learningModel = learningModel;
            return model;
        }
        public async Task<Af4dec16_x002D_0900_x002D_4f35_x002D_8d03_x002D_28743433843e_cb36632a_x002D_770d_x002D_4ef8_x002D_a996_x002D_d3b067ff0e11ModelOutput> EvaluateAsync(Af4dec16_x002D_0900_x002D_4f35_x002D_8d03_x002D_28743433843e_cb36632a_x002D_770d_x002D_4ef8_x002D_a996_x002D_d3b067ff0e11ModelInput input) {
            Af4dec16_x002D_0900_x002D_4f35_x002D_8d03_x002D_28743433843e_cb36632a_x002D_770d_x002D_4ef8_x002D_a996_x002D_d3b067ff0e11ModelOutput output = new Af4dec16_x002D_0900_x002D_4f35_x002D_8d03_x002D_28743433843e_cb36632a_x002D_770d_x002D_4ef8_x002D_a996_x002D_d3b067ff0e11ModelOutput();
            LearningModelBindingPreview binding = new LearningModelBindingPreview(learningModel);
            binding.Bind("data", input.data);
            binding.Bind("classLabel", output.classLabel);
            binding.Bind("loss", output.loss);
            LearningModelEvaluationResultPreview evalResult = await learningModel.EvaluateAsync(binding, string.Empty);
            return output;
        }
    }
}
