<template>
  <div class="bg-green-200 py-32 px-10 min-h-screen ">
    <div class="bg-white p-10 md:w-3/4 lg:w-1/2 mx-auto">
      <form action="">

        <div class="flex items-center mb-5">
          <label for="name" class="inline-block w-20 mr-6 font-bold text-gray-600">Title</label>
          <input type="text" id="name" name="name" placeholder="Name" 
               class="flex-1 py-2 border-b-2 border-gray-400 
                      text-gray-600 placeholder-gray-400
                      outline-none">
          </div>

          <div class="flex items-center mb-10">
            <label for="twitter" class="inline-block w-20 mr-6  font-bold text-gray-600">Description</label>
            <input type="text" id="twitter" name="twitter" placeholder="twitter" 
                  class="flex-1 py-2 border-b-2 border-gray-400  
                          text-gray-600
                          placeholder-gray-400">  <!-- check other class spec upper section -->
          </div>

        <div class="text-right">
          <button class="py-3 px-8 bg-green-400 text-white font-bold">Submit</button> 
        </div>
      </form>
    </div>
  </div>
</template>


<script>
export default {
    name: "HomeView",
  };

</script>