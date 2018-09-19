from coremltools.models.utils import load_spec
from winmltools import convert_coreml
from winmltools.utils import save_model

# Load model file
model_coreml = load_spec('ContosoIT.mlmodel')

# Convert it!
# The automatic code generator (mlgen) uses the name parameter to generate class names.
model_onnx = convert_coreml(model_coreml, name='ContosoIT')

# Save the produced ONNX model in binary format
save_model(model_onnx, 'ContosoIT.onnx')  