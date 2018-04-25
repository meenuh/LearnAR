from flask import Flask, jsonify, request, make_response

app = Flask(__name__)

images = [
    {
        'id': 1,
        'imageName': 'dsajbflbfwobsbdcjsabiADIUEBFSKLJBFSHJBA'
    },
    {
        'id': 2,
        'imageName': 'sdbfuerhgfyubfdjbvuhbuoabfoabguoegbshai'
    }
]


@app.route('/')
def index():
    return "Hello World!"


@app.route('/circuit/image', methods=['GET'])
def get_image():
    return jsonify({'images': images})


@app.route('/circuit/image', methods=['POST'])
def post_image():
    # !if not request.json or not 'imageName' in request.json:
    # !abort(400)
    image = request.json['image']

    # call yolo stuff here
    # images.append(image)
    print(image)

    # we should NOT return the image back, but instead return the new jsonobject with 411 from YOLO
    return jsonify({'image': 'it was sent'}), 201


if __name__ == '__main__':
    app.run(debug=True)
