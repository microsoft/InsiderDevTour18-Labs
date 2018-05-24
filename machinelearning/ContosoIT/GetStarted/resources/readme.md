# Build 2018: Adding Visual Intelligence to your UWP application

This lab will introduce you to the WinML, a new set of APIs that let developers harness the full capabilities of any Windows 10 device to run ONNX-based machine learning models. ONNX, or the Open Neural Network Exchange, is an open source format for AI models which has either first or third party support for all the major deep learning toolkits.

In this session, we'll show you how a UWP app can harness this capability to provide computer vision capabilities at the edge using an ONNX-based model developed in the cloud. We'll add local computer vision capabilities to an app used by the fictional *Contoso IT* organization to help technicians more easily identify different types of hardware they might need to service in the field.

## Getting Started

### A) System Requirements

WinML will be part of the next major update to Windows 10; however, it can already be used if you are part of the Windows Insider Program ([learn more](https://insider.microsoft.com)). The accompanying Insider SDK is also required along with Visual Studio 2017.

ONNX support in Visual Studio 2017 requires version 15.7 or later (currently in preview). Ensure you use the preview version of Visual Studio that's installed in the lab VM.

WinMLTools is a Python package provided by Microsoft to convert models to the ONNX format.

The lab environment has been pre-configured for you with all of the required prerequisites.

### B) Set up your account

The Custom Vision portal **requires** either a work or school account (e.g. Office 365 accounts) or a personal Microsoft account (e.g. `@outlook.com`). You can create a new personal Microsoft account [here](https://signup.live.com) (signup.live.com) with an existing email address (or create a new one). For this lab environment, you have been provided with Office 365 credentials to use. You can also find these on the resources tab in the lab UI.

User: @lab.CloudPortalCredential(1).Username

Password: @lab.CloudPortalCredential(1).Password

### D) Explore the current app

A partially completed build of the *Contoso IT* app has been provided.

1. Open **Visual Studio 2017 Preview** from the Start menu.

    > Make sure you select the *Preview* version.
1. Click **Open Project/Solution** from the **File > Open** menu.
1. **Select** the solution file `Desktop\WinML lab\src\ContosoIT.sln` and wait for it to load.
1. **Build** the solution from the **Build > Build Solution** menu item.
1. Wait for the NuGet packages to restore and for the solution to complete building. This may take a moment.
1. **Start** the app by clicking on **Local Machine** in the toolbar.

    > If the button is disabled, the solution is still building.
1. Once the app starts, you'll be able to see the options for providing an image for classification.
1. Click **Attach image**.
1. Select the `pro-test.jpg` file in `Desktop\WinML lab\resources\prediction\surface-pro` folder.
1. Wait for the app to display the knowledge base for the device.

The app lacks any computer vision capabilities so it will always classify images as containing a Surface Pro. The rest of the lab will take you through how to implement this missing feature.

## Building the ONNX model

### A) Create a Custom Vision project

Before we can start training our classifier, we need to create a project in the Custom Vision portal. The Custom Vision Service provides a free tier that allows us to create two projects before having to move to a transaction-based model.

1. Open **Microsoft Edge** and navigate to the [Custom Vision](https://customvision.ai/) (customvision.ai) portal.
1. **Log in** using a valid account (learn more above).
1. If this is your first time visiting the portal, it will request some permissions. Click the **Yes** button to agree (you can revoke these permissions later if necessary).
1. If this is your first time visiting the portal, it will also prompt you to agree with the terms and conditions. **Check** the box to indicate consent and then click the **I agree** button.
1. Click **New Project**.
1. Provide the required values:
    * Name: `winmllab`
    * Description: `WinML lab`
    * Domains: `General (compact)`
1. Click **Create Project**.

### B) Add images

Training an image classifier requires example images. While our data set is relatively small and can be uploaded via a web browser, it's likely that production scenarios will require hundreds or thousands of images and thus are best uploaded via the API.

1. Click the **Training Images** tab at the top of the page.
1. Click the **Add images** button.
1. Click the **Browse local files** button.
1. Browse to `Desktop\WinML lab\resources\training\surface-pro` and select **all** of the images.
1. Click **Open**.
1. In the **Add some tags to this batch of images** field, enter the tag `surface-pro`.
1. Click on the **+** button next to the field.
1. Click the **Upload 15 files** button.
1. Wait for the upload to complete. This can take a few moments.
1. Click the **Done** button.
1. Click the **Add images** button.
1. Click the **Browse local files** button.
1. Browse to `Desktop\WinML lab\resources\training\surface-studio` and select **all** of the images.
1. Click **Open**.
1. In the **Add some tags to this batch of images** field, enter the tag `surface-studio`.
1. Click on the **+** button next to the field.
1. Click the **Upload 15 files** button.
1. Wait for the upload to complete. This can take a few moments.
1. Click **Done**.

### C) Train the classifier

Now that our data set is available, and each image correctly tagged, the training process can begin. This is a quick operation given the small size of our data set. The output is a classifier that can be used to make predictions. As more training data is added, we can create a new iteration of the classifier and analyze its performance.

1. Click on the green **Train** button in the top nav bar.
1. Wait for the training to complete.
1. Review the new classifier's precision and recall. **Mouse over** the tooltips to learn more about these concepts.

> It's recommended that at least 30 images of each class or category are included for a prototype classifier. In the interests of time, we're only providing 15 images for each class so we should expect slightly lower accuracy compared to a fully trained version.

### D) Test the classifier

The portal provides a test page for us to do a basic smoke test of our model with some new, unseen data. In a production scenario, the RESTful API is a more appropriate mechanism for doing bulk predictions.

1. Click on the **Quick Test** button in the top nav bar.
1. Click the **Browse local files** button.
1. Browse to `Desktop\WinML lab\resources\prediction\surface-pro` and select the `pro_test.jpg` image.
1. Click **Open**.
1. Wait for the classification to run and ensure it is correctly recognized as a `surface-pro`.
1. Click the **Browse local files** button.
1. Browse to `Desktop\WinML lab\resources\prediction\surface-studio` and select the `studio_test.jpg` image.
1. Click **Open**.
1. Wait for the classification to run and ensure it is correctly recognized as a `surface-studio`.
1. Close the **Quick Test** side bar by clicking on the **X** icon in the top right.
1. Click on the **Predictions** tag in the top nav bar.
1. Here we can see the new test data. If we click on an image, we can manually tag it and add it to our data set. This image will then be included next time we re-train our classifier, potentially leading to improved accuracy.

### E) Export the model

Now that our classifier's complete, it's time to export it to the ONNX format for use with our UWP app.

1. Click on the **Performance** tab at the top of the page.
1. Click on the **Export** button.
1. Click on **CoreML** when asked for the desired format.

    > In this lab, we'll opt for a model in the CoreML format so we can show you how to use WinMLTools to convert it into ONNX. In general though, you should prefer native ONNX models where available. If you'd like to skip the section on WinMLTools, download the model as a native ONNX model instead.
1. Click **Export**. This might take a few seconds.
1. Click **Download** and then **Save** to the `Desktop\WinML lab` folder when prompted.

## Converting the CoreML model to ONNX

### A) Ensure tools are installed

WinMLTools is a Python package built by Microsoft to convert a variety of common deep learning model formats to ONNX. For this lab, your environment has been preconfigured and WinMLTools is already installed. We have also preinstalled a Python package called CoreMLTools, which we will use to load the CoreML format we exported from Custom Vision.

### B) Convert the Custom Vision model to ONNX

In this section, we'll use the tools to convert our model. We'll use the interactive Python shell (but you could just as easily write a short `.py` script to complete this task).

1. Open a **Command Prompt** from the Start menu.
1. **Change** directory to `Desktop\Win ML lab`.

    ```
    cd "Desktop\Win ML lab"
    ```
1. Start **Python** by running `python`.
1. **Import** the `load_spec` function to load the CoreML model.

    ```py
    from coremltools.models.utils import load_spec
    ```
1. Use the new function to **load** the model you downloaded from Custom Vision.

    ```py
    model_coreml = load_spec('your-model-name-here.mlmodel')
    ```
1. **Import** the `convert_coreml` function to convert the model into the ONNX format.

    ```py
    from winmltools import convert_coreml
    ```
1. Use the new function **convert** the model.

    ```py
    model_onnx = convert_coreml(model_coreml, name='Surface')
    ```
1. **Import** the `save_model` function to write the ONNX model to disk.

    ```py
    from winmltools.utils import save_model
    ```
1. Use the new function to **save** the model.

    ```py
    save_model(model_onnx, 'Surface.onnx')
    ```

### C) Explore the models

Optionally, you can continue using the Python shell to better understand the characteristics of the model.

1. **Invoke** the `description` property to see inputs and outputs for the CoreML model.

    ```py
    model_coreml.description
    ```

    > You'll see the input and output properties present in the generated code in Visual Studio in the next section.
1. **Invoke** the `input` and `output` properties to see the inputs and outputs for the ONNX version of the model.

    ```py
    model_onnx.graph.input
    model_onnx.graph.output
    ```

    > While the structure is different to CoreML, the inputs and outputs of the model are same in the ONNX version (as you'd expect).

## Using the ONNX model in your app

### A) Add the model to the project

Now that the ONNX model is downloaded, we need to add it to the project. 

1. Return to **Visual Studio**.
1. In the **Solution Explorer** panel, right-click on the project, and choose the **Add > Existing Item...** option.
1. Find your **ONNX** model in `Desktop\WinML lab` folder and add it to the project.
1. Right-click on the ONNX model file and click on the **Properties** option.
1. Set the **Build Action** property to be *Content*.

    > This ensures the model is included as part of the application at build time.

### B) Review the wrapper classes

In Visual Studio 2017 (15.7) or later, importing an ONNX model to your project will result in the generation of three wrapper classes.

1. **Open** the generated code file in the root of your solution. If you following the naming patterns when converting the model to ONNX, it will be called `Surface.cs`.
1. Review the `SurfaceModelInput` class. It holds the image that we're going to run through the **ONNX** model.

    ```cs
    public sealed class SurfaceModelInput
    {
        public VideoFrame data { get; set; }
    }
    ```
1. Review the `SurfaceModelOutput` class. This class represents the result from running the input data through the **ONNX** model.

    ```cs
    public sealed class SurfaceModelOutput
    {
        public IList<string> classLabel { get; set; }
        public IDictionary<string, Float> loss { get; set; }

        public SurfaceModelOutput()
        {
            this.classLabel = new List<string>();
            this.loss = new Dictionary<string, float>
            {
                { "surface-pro", float.NaN },
                { "surface-studio", float.NaN }
            };
        }
    }
    ```

    > After analyzing an image, **ClassLabel** will hold all the tags associated to the Input data and **Loss** will hold the probabilities corresponding to all the tags in the **ONNX** model. For example, if we run through an image of a Surface Pro, then the `surface-pro` element in the **Loss** dictionary might have a 0.99987 value, and the `surface-studio` element might have a 0.00013 value.
1. Review the `SurfaceModel` class.

    ```cs
    public sealed class SurfaceModel
    {
        private LearningModelPreview learningModel;
    
        public static async Task<SurfaceModel> CreateSurfaceModel(StorageFile file)
        {
            LearningModelPreview learningModel = await LearningModelPreview.LoadModelFromStorageFileAsync(file);
            SurfaceModel model = new SurfaceModel();
            model.learningModel = learningModel;
            return model;
        }

        public async Task<SurfaceModelOutput> EvaluateAsync(SurfaceModelInput input)
        {
            SurfaceModelOutput output = new SurfaceModelOutput();
            LearningModelBindingPreview binding = new LearningModelBindingPreview(learningModel);
            binding.Bind("data", input.data);
            binding.Bind("classLabel", output.classLabel);
            binding.Bind("loss", output.loss);
            LearningModelEvaluationResultPreview evalResult = await learningModel.EvaluateAsync(binding, string.Empty);
            return output;
        }
    }
    ```

    > The **CreateSurfaceModel** method creates a model for making predictions. It receives the ONNX model file path and then it initializes the **learningModel** field with the ONNX model from disk.

    > The **EvaluateAsync** method is used to make predictions. It's a wrapper for the **learningModel.EvaluateAsync** method. It binds the input image and the output parameters before asynchronously invoking the model.

### C) Call the wrapper classes

Now that the model has been imported and wrapper classes generated, it's time to integrate this new code into our app.

1. Open **DevicesPage.xaml.cs**.
1. **Scroll down** until you find the `// Your code goes here` comment. The code written in the forthcoming steps should be placed here.
1. Fetch a reference to the model file by **adding** the following code snippet. Our model is located in the same place as the rest of the app installation.

    ```cs
    var modelFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri($"ms-appx:///{DetectionConstants.OnnxModelFileName}"));
    ```
1. Pass the model file to the wrapper class to load and prepare it for making predictions by **adding** the following code snippet.

    ```cs
    var model = await SurfaceModel.CreateSurfaceModel(modelFile);
    ```
1. Prepare the image input by **adding** the following code snippet.

    ```cs
    var modelInput = new SurfaceModelInput
    {
        data = await ConvertFileToVideoFrameAsync(detectionDataParameters.SelectedFile)
    };
    ```

    > The `ConvertFileToVideoFrameAsync` method is provided for you and converts an image to a video frame (as required by the model).
1. Invoke the model with our image input and capture the results by **adding** the following code snippet.

    ```cs
    var output = await model.EvaluateAsync(modelInput);
    ```
1. **Replace** the following code snippet:

    ```cs
    var classLabel = "surface-pro";
    ```
    **with** the following code snippet to extract the correct classification:

    ```cs
    var classLabel = output.classLabel.FirstOrDefault();
    ```

### D) Test the new functionality

The model and app code changes are complete. In this section, we'll debug through this new capability to see it in action.

1. **Add** a breakpoint to the start of the `BeginDetection` method in `Devices.xaml.cs`.
1. Click the **Local Machine** button in the toolbar to start local debugging.
1. Once the app starts, click **Attach image**.
1. Select the `pro-test.jpg` file in `Desktop\WinML lab\resources\prediction\surface-pro` folder.
1. **Wait** for the `BeginDetection` breakpoint to be hit.
1. **Step into** and then through the `CreateSurfaceModel` method.
1. **Step into** the `EvaluateAsync` method.
1. **Step over** the first `Bind` method and **mouse over** the `input.data` field to see the input.
1. **Step over** the second `Bind` method and **mouse over** the `output.classLabel` field to see the output before evaluation.
1. **Step over** the latest `Bind` method and **mouse over** the `output.loss` field to see the output before evaluation.
1. **Step over** until you reach the end of the `EvaluateAsync` method.
1. **Mouse over** the `output.classLabel` and `output.loss` fields to see how they have changed.

    > Observe how a value is returned for all possible classifications, not just the 'winner'. The value closest to 1.0 is considered the most likely classification.
1. **Mouse over** the `evalResult` variable to see what else is returned by the evaluation.
1. Click the **Continue** button in the toolbar and return to the app.
1. Repeat the process for the `studio-test.jpg` file in `Desktop\WinML lab\resources\prediction\surface-studio` folder.

## Conclusion

In this lab, you've seen how the cloud - in the form of the Custom Vision Service - can be used to quickly build powerful models and how the intelligent edge - in the form of Windows and WinML - can be used to execute them using the full capabilities of the device.