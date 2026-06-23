import ProductForm from './components/products/ProductForm';

export default function HomePage() {
  return (
    <main className="flex min-h-screen flex-col items-center justify-center p-6 bg-gray-50">
      <div className="mb-8 text-center">
        <h1 className="text-4xl font-extrabold text-gray-900 tracking-tight">SmartStore Dashboard</h1>
        <p className="mt-2 text-gray-600">Enterprise Clean Architecture Frontend</p>
      </div>
      
      {/* استدعاء المكون المعزول */}
      <ProductForm />
    </main>
  );
}
