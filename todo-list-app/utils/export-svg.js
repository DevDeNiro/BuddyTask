export function saveSVGs() {
  const svgFiles = [
    "../public/complet.svg",
    "../public/uncomplet.svg",
    "../public/pen.svg",
    "../public/delete.svg",
  ];

  svgFiles.forEach((filePath) => {
    fetch(filePath)
      .then((response) => response.blob())
      .then((blob) => {
        localStorage.setItem(filePath, URL.createObjectURL(blob));
      });
  });
}

saveSVGs();
