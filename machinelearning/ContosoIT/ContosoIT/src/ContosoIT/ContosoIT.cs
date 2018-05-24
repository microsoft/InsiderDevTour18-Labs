using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Media;
using Windows.Storage;
using Windows.AI.MachineLearning.Preview;

// af4dec16-0900-4f35-8d03-28743433843e_cb36632a-770d-4ef8-a996-d3b067ff0e11

namespace ContosoIT
{
    public sealed class ContosoITModelInput
    {
        public VideoFrame data { get; set; }
    }

    public sealed class ContosoITModelOutput
    {
        public IList<string> classLabel { get; set; }
        public IDictionary<string, float> loss { get; set; }
        public ContosoITModelOutput()
        {
            this.classLabel = new List<string>();
            this.loss = new Dictionary<string, float>()
            {
                { "Surface Pro", float.NaN },
                { "Surface Studio", float.NaN },
            };
        }
    }

    public sealed class ContosoITModel
    {
        private LearningModelPreview learningModel;
        public static async Task<ContosoITModel> CreateContosoITModel(StorageFile file)
        {
            LearningModelPreview learningModel = await LearningModelPreview.LoadModelFromStorageFileAsync(file);
            ContosoITModel model = new ContosoITModel();
            model.learningModel = learningModel;
            return model;
        }
        public async Task<ContosoITModelOutput> EvaluateAsync(ContosoITModelInput input) {
            ContosoITModelOutput output = new ContosoITModelOutput();
            LearningModelBindingPreview binding = new LearningModelBindingPreview(learningModel);
            binding.Bind("data", input.data);
            binding.Bind("classLabel", output.classLabel);
            binding.Bind("loss", output.loss);
            LearningModelEvaluationResultPreview evalResult = await learningModel.EvaluateAsync(binding, string.Empty);
            return output;
        }
    }
}
