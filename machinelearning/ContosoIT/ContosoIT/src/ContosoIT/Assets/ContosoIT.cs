// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

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
