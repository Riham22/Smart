'use client';

import React, { useState, useTransition } from 'react';
import api from '../../services/api';

export default function ProductForm() {
  const [formData, setFormData] = useState({
    name: '',
    sku: '',
    description: '',
    price: 0,
    stockQuantity: 0,
    categoryId: '3fa85f64-5717-4562-b3fc-2c963f66afa6', // Temporary MVP GUID
  });

  const [isPending, startTransition] = useTransition(); // لرفع أداء العمليات غير المتزامنة دون توقف واجهة المستخدم
  const [message, setMessage] = useState<{ type: 'success' | 'error'; text: string } | null>(null);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setMessage(null);

    // استخدام startTransition يخبر React بأن هذه العملية يمكن جدولتها دون تجميد الشاشة
    startTransition(async () => {
      try {
        const response = await api.post('/Products', formData);
        setMessage({
          type: 'success',
          text: `Success! Product Created with ID: ${response.data.id}`,
        });
        
        // إعادة تعيين النموذج بعد النجاح لرفع تجربة المستخدم
        setFormData({
          name: '',
          sku: '',
          description: '',
          price: 0,
          stockQuantity: 0,
          categoryId: '3fa85f64-5717-4562-b3fc-2c963f66afa6',
        });
      } catch (error) {
        console.error(error);
        setMessage({
          type: 'error',
          text: 'Failed to connect to server. Check your Backend or CORS policy.',
        });
      }
    });
  };

  return (
    <div className="w-full max-w-md p-8 bg-white rounded-xl shadow-lg border border-gray-200">
      <h2 className="text-2xl font-bold text-gray-800 mb-6 text-center">Add New Product (MVP)</h2>

      <form onSubmit={handleSubmit} className="space-y-4">
        <div>
          <label className="block text-sm font-semibold text-gray-700">Product Name</label>
          <input
            type="text"
            required
            value={formData.name}
            className="mt-1 block w-full rounded-md border border-gray-300 p-2 text-gray-900 focus:ring-2 focus:ring-blue-500 focus:outline-none"
            onChange={(e) => setFormData({ ...formData, name: e.target.value })}
          />
        </div>

        <div>
          <label className="block text-sm font-semibold text-gray-700">SKU (Stock Keeping Unit)</label>
          <input
            type="text"
            required
            value={formData.sku}
            className="mt-1 block w-full rounded-md border border-gray-300 p-2 text-gray-900 focus:ring-2 focus:ring-blue-500 focus:outline-none"
            onChange={(e) => setFormData({ ...formData, sku: e.target.value })}
          />
        </div>

        <div>
          <label className="block text-sm font-semibold text-gray-700">Description</label>
          <textarea
            value={formData.description}
            className="mt-1 block w-full rounded-md border border-gray-300 p-2 text-gray-900 focus:ring-2 focus:ring-blue-500 focus:outline-none"
            onChange={(e) => setFormData({ ...formData, description: e.target.value })}
          />
        </div>

        <div className="grid grid-cols-2 gap-4">
          <div>
            <label className="block text-sm font-semibold text-gray-700">Price</label>
            <input
              type="number"
              required
              min="0"
              step="0.01"
              value={formData.price || ''}
              className="mt-1 block w-full rounded-md border border-gray-300 p-2 text-gray-900 focus:ring-2 focus:ring-blue-500 focus:outline-none"
              onChange={(e) => setFormData({ ...formData, price: parseFloat(e.target.value) || 0 })}
            />
          </div>
          <div>
            <label className="block text-sm font-semibold text-gray-700">Stock Qty</label>
            <input
              type="number"
              required
              min="0"
              value={formData.stockQuantity || ''}
              className="mt-1 block w-full rounded-md border border-gray-300 p-2 text-gray-900 focus:ring-2 focus:ring-blue-500 focus:outline-none"
              onChange={(e) => setFormData({ ...formData, stockQuantity: parseInt(e.target.value) || 0 })}
            />
          </div>
        </div>

        <button
          type="submit"
          disabled={isPending}
          className="w-full mt-2 bg-blue-600 text-white p-2.5 rounded-md font-medium hover:bg-blue-700 disabled:bg-gray-400 transition-all shadow-sm"
        >
          {isPending ? 'Submitting to CQRS...' : 'Submit Product'}
        </button>
      </form>

      {message && (
        <div
          className={`mt-4 p-3 rounded-md text-center text-sm font-medium ${
            message.type === 'success' ? 'bg-green-50 text-green-800 border border-green-200' : 'bg-red-50 text-red-800 border border-red-200'
          }`}
        >
          {message.text}
        </div>
      )}
    </div>
  );
}
