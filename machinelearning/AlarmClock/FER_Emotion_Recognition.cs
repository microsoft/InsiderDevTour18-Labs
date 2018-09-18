using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Media;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.AI.MachineLearning;
namespace AlarmClock
{
    
    public sealed class FER_Emotion_RecognitionInput
    {
        public ImageFeatureValue Input3; // shape(1,1,64,64)
    }
    
    public sealed class FER_Emotion_RecognitionOutput
    {
        public TensorFloat Plus692_Output_0; // shape(1,8)
    }
    
    public sealed class FER_Emotion_RecognitionModel
    {
        private LearningModel model;
        private LearningModelSession session;
        private LearningModelBinding binding;
        public static async Task<FER_Emotion_RecognitionModel> CreateFromStreamAsync(IRandomAccessStreamReference stream)
        {
            FER_Emotion_RecognitionModel learningModel = new FER_Emotion_RecognitionModel();
            learningModel.model = await LearningModel.LoadFromStreamAsync(stream);
            learningModel.session = new LearningModelSession(learningModel.model);
            learningModel.binding = new LearningModelBinding(learningModel.session);
            return learningModel;
        }
        public async Task<FER_Emotion_RecognitionOutput> EvaluateAsync(FER_Emotion_RecognitionInput input)
        {
            binding.Bind("Input3", input.Input3);
            var result = await session.EvaluateAsync(binding, "0");
            var output = new FER_Emotion_RecognitionOutput();
            output.Plus692_Output_0 = result.Outputs["Plus692_Output_0"] as TensorFloat;
            return output;
        }
    }
}
