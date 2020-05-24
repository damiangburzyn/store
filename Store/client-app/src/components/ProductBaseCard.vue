<template>
    <div>
        <div>

            <img src="{{ ProductItem.Image }}" />
        </div>
        <div>
            {{ ProductItem.Title }}
        </div>
        <div>
            {{ ProductItem.Description }}
        </div>

    </div>



</template>

<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';
import { Product } from '@/store/models';
@Component
export default class HelloWorld extends Vue {
  @Prop() private msg!: string;
  @Prop() public ProductItem: Product|null = null;
}
</script>