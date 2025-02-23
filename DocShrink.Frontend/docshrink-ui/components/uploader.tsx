"use client";

import { useState } from "react";
import { useDropzone } from "react-dropzone";
import { FiUploadCloud, FiFile, FiDownload } from "react-icons/fi";
import { Button } from "@/components/ui/button";
import { Progress } from "@/components/ui/progress";
import { useToast } from "@/hooks/use-toast";

interface CompressedFile {
  fileName: string;
  originalSize: number;
  compressedSize: number;
  downloadUrl: string;
}

export function Uploader() {
  const [files, setFiles] = useState<File[]>([]);
  const [uploading, setUploading] = useState(false);
  const [uploadProgress, setUploadProgress] = useState(0);
  const [compressedFile, setCompressedFile] = useState<CompressedFile | null>(null);
  const { toast } = useToast();

  const onDrop = (acceptedFiles: File[]) => {
    const validFiles = acceptedFiles.filter((file) =>
      ["application/pdf", "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"].includes(file.type)
    );

    if (validFiles.length !== acceptedFiles.length) {
      toast({
        title: "Invalid file type",
        description: "Only PDF, DOCX, and XLSX files are allowed.",
        variant: "destructive",
      });
    }

    setFiles(validFiles);
  };

  const { getRootProps, getInputProps, isDragActive } = useDropzone({ onDrop });

  const uploadFiles = async () => {
    if (files.length === 0) {
      toast({
        title: "No file selected",
        description: "Please select a file before uploading.",
        variant: "destructive",
      });
      return;
    }

    try {
      const formData = new FormData();
      formData.append("file", files[0]);

      setUploading(true);
      setUploadProgress(0);

      const response = await fetch("http://localhost:5241/api/v1/files/compress", {
        method: "POST",
        body: formData,
      });

      if (!response.ok) throw new Error("Failed to upload file");

      const data: CompressedFile = await response.json();
      setCompressedFile(data);
      setFiles([]);
    } catch (error) {
      console.error(error);
      toast({
        title: "Upload failed",
        description: "An error occurred during upload.",
        variant: "destructive",
      });
    } finally {
      setUploading(false);
    }
  };

  return (
    <div className="space-y-4">
      <div
        {...getRootProps()}
        className={`border-2 border-dashed rounded-lg p-8 text-center cursor-pointer transition-colors duration-200 ${
          isDragActive ? "border-blue-400 bg-blue-50" : "border-gray-300"
        }`}
      >
        <input {...getInputProps()} />
        <FiUploadCloud className="mx-auto mb-4 text-blue-500" size={48} />
        <p className="text-xl mb-2 text-gray-700">
          Drag & drop a file here, or click to select a file
        </p>
        <p className="text-sm text-gray-500">
          Supported formats: PDF, DOCX, XLSX
        </p>
      </div>

      {files.length > 0 && (
        <div className="bg-white rounded-lg p-6 shadow-sm">
          <h3 className="font-semibold mb-4 text-gray-700">Selected File:</h3>
          <ul className="space-y-2">
            {files.map((file) => (
              <li key={file.name} className="flex items-center text-gray-600">
                <FiFile className="mr-2 text-blue-500" />
                <span>{file.name}</span>
              </li>
            ))}
          </ul>
          <Button onClick={uploadFiles} disabled={uploading} className="mt-6 w-full">
            {uploading ? "Processing..." : "Upload & Compress"}
          </Button>
        </div>
      )}

      {uploading && (
        <div className="bg-white rounded-lg p-6 shadow-sm">
          <Progress value={uploadProgress} className="w-full" />
          <p className="mt-4 text-center text-sm text-gray-600">
            Uploading your file...
          </p>
        </div>
      )}

      {compressedFile && (
        <div className="bg-white rounded-lg p-6 shadow-sm mt-8">
          <h2 className="text-2xl font-semibold mb-6 text-gray-800">Compressed File</h2>
          <div className="flex items-center justify-between mb-4">
            <div className="flex items-center space-x-3">
              <FiFile className="text-blue-500" size={24} />
              <span className="text-lg font-medium text-gray-700">{compressedFile.fileName}</span>
            </div>
            <a href={compressedFile.downloadUrl} target="_blank" rel="noopener noreferrer">
              <Button variant="outline" size="sm" className="text-blue-500 border-blue-500 hover:bg-blue-50">
                <FiDownload className="mr-2" />
                Download
              </Button>
            </a>
          </div>
          <div className="grid grid-cols-3 gap-4 mb-4">
            <div className="text-sm text-gray-600">
              <p className="font-semibold">Original Size</p>
              <p>{(compressedFile.originalSize / 1024 / 1024).toFixed(2)} MB</p>
            </div>
            <div className="text-sm text-gray-600">
              <p className="font-semibold">Compressed Size</p>
              <p>{(compressedFile.compressedSize / 1024 / 1024).toFixed(2)} MB</p>
            </div>
            <div className="text-sm text-gray-600">
              <p className="font-semibold">Reduction</p>
              <p>{((1 - compressedFile.compressedSize / compressedFile.originalSize) * 100).toFixed(1)}%</p>
            </div>
          </div>
        </div>
      )}
    </div>
  );
}


