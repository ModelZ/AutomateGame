import cv2
import sys

# Callback function for mouse events
def click_event(event, x, y, flags, param):
    if event == cv2.EVENT_LBUTTONDOWN:
        print(f"Clicked at ({x}, {y})")

        # If you need the color at the clicked pixel
        color = img[y, x]
        print(f"Color at clicked pixel: {color}")

# Check if the correct number of command line arguments is provided
if len(sys.argv) != 2:
    print("Usage: python3 img_coord_finder.py <image_filename>")
    sys.exit(1)

# Read the image
img = cv2.imread(sys.argv[1])

# Check if the image is loaded successfully
if img is None:
    print(f"Error: Unable to read the image '{sys.argv[1]}'")
    sys.exit(1)

# Display the image
cv2.imshow("Image", img)

# Set the callback function for mouse events
cv2.setMouseCallback("Image", click_event)

# Wait for a key press and close the window
cv2.waitKey(0)
cv2.destroyAllWindows()
