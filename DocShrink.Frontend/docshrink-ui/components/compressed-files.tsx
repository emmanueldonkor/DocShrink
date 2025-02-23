'use client';

import { FiFileText, FiDownload } from 'react-icons/fi';
import { Button } from '@/components/ui/button';
import { motion } from 'framer-motion';

interface CompressedFile {
  fileName: string;
  originalSize: number;
  compressedSize: number;
  downloadUrl: string;
}

interface CompressedFilesProps {
  file: CompressedFile;
}

export function CompressedFiles({ file }: CompressedFilesProps) {
  return (
    <div className="bg-white rounded-lg p-6 shadow-sm">
      <h2 className="text-2xl font-semibold mb-6 text-gray-800">Compressed File</h2>
      <motion.div
        initial={{ opacity: 0, y: 20 }}
        animate={{ opacity: 1, y: 0 }}
        transition={{ duration: 0.3 }}
        className="border border-gray-200 rounded-lg p-4 hover:shadow-md transition-shadow duration-200"
      >
        <div className="flex items-center justify-between mb-4">
          <div className="flex items-center space-x-3">
            <FiFileText className="text-blue-500" size={24} />
            <span className="text-lg font-medium text-gray-700">{file.fileName}</span>
          </div>
          <Button variant="outline" size="sm" className="text-blue-500 border-blue-500 hover:bg-blue-50">
            <FiDownload className="mr-2" />
            <a href={file.downloadUrl} target="_blank" rel="noopener noreferrer">Download</a>
          </Button>
        </div>
        <div className="grid grid-cols-3 gap-4 mb-4">
          <div className="text-sm text-gray-600">
            <p className="font-semibold">Original Size</p>
            <p>{(file.originalSize / 1024 / 1024).toFixed(2)} MB</p>
          </div>
          <div className="text-sm text-gray-600">
            <p className="font-semibold">Compressed Size</p>
            <p>{(file.compressedSize / 1024 / 1024).toFixed(2)} MB</p>
          </div>
          <div className="text-sm text-gray-600">
            <p className="font-semibold">Reduction</p>
            <p>{((1 - file.compressedSize / file.originalSize) * 100).toFixed(1)}%</p>
          </div>
        </div>
      </motion.div>
    </div>
  );
}
